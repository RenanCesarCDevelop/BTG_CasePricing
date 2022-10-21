using AspNetCoreRateLimit;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using Swashbuckle.AspNetCore.Filters;
//using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using BTG.CasePricing.Application.Commands.GitRepositorios;
using BTG.CasePricing.Application.Common.Behaviours;
using BTG.CasePricing.Application.Settings;
using BTG.CasePricing.Domain.Constants;
using BTG.CasePricing.Domain.Interfaces.HttpClients;
using BTG.CasePricing.Domain.Util.Mappings;
using BTG.CasePricing.Infrastructure.HttpFactory;
using BTG.CasePricing.Infrastructure.HttpFactory.Abstractions;
using BTG.CasePricing.Infrastructure.Swagger;
using BTG.CasePricing.Domain.Interfaces.Services.GitRepositorios;
using BTG.CasePricing.Application.Tenant;
using BTG.CasePricing.Domain.Interfaces.Tenant;

namespace BTG.CasePricing.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtension
{
    public static IServiceCollection Configure(this IServiceCollection services, IConfiguration config)
    {
        services.AddApplication();
        services.AddInfrastructure(config);
        return services;
    }

    private static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<IGitHubApiHttpClient, GitHubApiHttpClient>();
        services.AddScoped<IGitRepositorioService, GitRepositorioService>();
        services.AddScoped<ITenantProvider, TenantProvider>();

        services.AddSingleton<IMapper>((m) =>
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            return configuration.CreateMapper();
        });

        return services;
    }

    private static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddCacheService();
        services.AddIpRatingLimit(config);
        services.AddControllers().AddFluentValidation();
        services.AddSwaggerAndApiVersioning();
        services.AddHealthChecks();
        services.AddMvc(x => x.EnableEndpointRouting = false);
        services.AddServices();
        services.AddSettings(config);
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddCors(co =>
            co.AddPolicy("CorsPolicy", cpb =>
                cpb.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

        services.AddJwtAuthentication(config);

        services.AddRefitClient<IExternalGitHubApi>()
            .ConfigureHttpClient((c) =>
            {
                c.BaseAddress = new System.Uri(Environment.GetEnvironmentVariable(EnvironmentVariablesConstants.GitHubBaseUrl));
                int.TryParse(Environment.GetEnvironmentVariable(EnvironmentVariablesConstants.GitHubTimeoutSegundos), out int timeout);
                if (timeout > 0)
                    c.Timeout = TimeSpan.FromSeconds(timeout);
            });

        return services;
    }


    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        JwtSettings jwtSettings = config.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.ValidIssuer,
                ValidAudience = jwtSettings.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            };
        });

        return services;
    }

    private static IServiceCollection AddIpRatingLimit(this IServiceCollection services, IConfiguration config)
    {
        IpRateLimitSettings ipRateLimitSettings = config.GetSection(nameof(IpRateLimitSettings)).Get<IpRateLimitSettings>();

        // Rate Limit
        if (ipRateLimitSettings.EnableEndpointRateLimiting)
        {
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(config.GetSection(nameof(IpRateLimitSettings)));
            services.Configure<IpRateLimitPolicies>(config.GetSection(nameof(IpRateLimitPolicies)));
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            if (ipRateLimitSettings.IsAppInLoadBalance)
            {
                services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
                services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
            }
            else
            {
                services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
                services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            }
        }

        return services;
    }

    private static IServiceCollection AddSwaggerAndApiVersioning(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.ExampleFilters();
            options.OperationFilter<AddRequiredHeaderParameter>();
            options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
            options.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Description = "Standard Authorization header using the Bearer scheme (JWT). Example: \"bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });
        });

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
        services.AddFluentValidationRulesToSwagger();
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddApiVersioning(option =>
        {
            option.DefaultApiVersion = new ApiVersion(1, 0);
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.ReportApiVersions = true;
            option.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

}