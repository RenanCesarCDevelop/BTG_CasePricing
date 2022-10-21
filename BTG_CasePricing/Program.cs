using BTG.CasePricing.Infrastructure.Extensions;
using BTG.CasePricing.WebAPI.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
    builder.Host.Configure();
    builder.Services.Configure(builder.Configuration);
    WebApplication app = builder.Build();
    app.Configure(builder.Configuration);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server shutting down...");
    Log.CloseAndFlush();
}