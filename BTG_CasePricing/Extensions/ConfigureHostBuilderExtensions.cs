using Serilog;
using System.Globalization;

namespace BTG.CasePricing.WebAPI.Extensions
{
    public static class ConfigureHostBuilderExtensions
    {
        public static ConfigureHostBuilder Configure(this ConfigureHostBuilder host)
        {
            host
                .AddCultureInfo()
                .AddConfigurationFiles()
                .AddSerilog();

            return host;
        }

        private static ConfigureHostBuilder AddConfigurationFiles(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                const string configurationsDirectory = "Configurations";
                IHostEnvironment env = context.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/cache.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/database.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/ipratelimit.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/security.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/serilog.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/swagger.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/api.json", optional: false, reloadOnChange: true)

                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/cache.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/database.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/ipratelimit.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/security.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/serilog.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/swagger.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"{configurationsDirectory}/api.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                if (env.IsDevelopment())
                {
                    config.AddUserSecrets(typeof(Program).Assembly, optional: true);
                }

                config.AddEnvironmentVariables();
            });

            return host;
        }

        private static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder host)
        {
            host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services));

            return host;
        }

        private static ConfigureHostBuilder AddCultureInfo(this ConfigureHostBuilder host)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

            return host;
        }
    }
}
