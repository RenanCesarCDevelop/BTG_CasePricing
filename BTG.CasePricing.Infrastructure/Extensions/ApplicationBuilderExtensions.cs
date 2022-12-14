#nullable enable

using AspNetCoreRateLimit;
using BTG.CasePricing.Application.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Diagnostics.CodeAnalysis;

namespace BTG.CasePricing.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder Configure(this IApplicationBuilder app, IConfiguration config)
    {
        const string CorsPoliceName = "CorsPolicy";

        if (config.GetSection(nameof(IpRateLimitSettings)).Get<IpRateLimitSettings>().EnableEndpointRateLimiting)
            app.UseIpRateLimiting();

        app.UseExceptionHandler("/error");
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            IApiVersionDescriptionProvider provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (string description in provider.ApiVersionDescriptions.Select(x => x.GroupName).Where(x => x != null))
            {
                options.SwaggerEndpoint($"/swagger/{description}/swagger.json", description.ToUpperInvariant());
            }
        });

        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseCors(CorsPoliceName);
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHealthChecks("/health");
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers().RequireCors(CorsPoliceName);
            endpoints.MapHealthChecks("/ready", new HealthCheckOptions()
            {
                Predicate = (check) => check.Tags.Contains("ready"),
            });
            endpoints.MapHealthChecks("/live", new HealthCheckOptions()
            {
                Predicate = (_) => false
            });
        });

        return app;
    }
}