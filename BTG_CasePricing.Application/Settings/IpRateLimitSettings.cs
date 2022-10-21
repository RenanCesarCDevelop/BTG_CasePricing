using BTG.CasePricing.Application.Interfaces;

namespace BTG.CasePricing.Application.Settings;

public class IpRateLimitSettings : IAppSettings
{
    public bool EnableEndpointRateLimiting { get; set; }
    public bool StackBlockedRequests { get; set; }
    public string RealIPHeader { get; set; }
    public string ClientIdHeader { get; set; }
    public int HttpStatusCode { get; set; }
    public List<string> IpWhitelist { get; set; }
    public List<string> EndpointWhitelist { get; set; }
    public List<string> ClientWhitelist { get; set; }
    public bool IsAppInLoadBalance { get; set; }
}