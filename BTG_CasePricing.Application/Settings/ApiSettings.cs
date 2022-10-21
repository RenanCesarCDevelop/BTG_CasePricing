using BTG.CasePricing.Application.Interfaces;

namespace BTG.CasePricing.Application.Settings;
public class ApiSettings : IAppSettings
{
    public int ConnectionString { get; set; }
    public string Authorization { get; set; }
    public string Origin { get; set; }
    public string Url { get; set; }
}
