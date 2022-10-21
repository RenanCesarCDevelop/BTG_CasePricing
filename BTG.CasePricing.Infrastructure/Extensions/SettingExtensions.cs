using BTG.CasePricing.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BTG.CasePricing.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class SettingExtensions
{
    internal static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration config)
    {
        return services
            .Configure<CacheSettings>(config.GetSection(nameof(CacheSettings)))
            .Configure<IpRateLimitSettings>(config.GetSection(nameof(IpRateLimitSettings)))
        .Configure<JwtSettings>(config.GetSection(nameof(JwtSettings)))
            .Configure<SwaggerSettings>(config.GetSection(nameof(SwaggerSettings)))
            .Configure<ApiSettings>(config.GetSection(nameof(ApiSettings)));
    }
}