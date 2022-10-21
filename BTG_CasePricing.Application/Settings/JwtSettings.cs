using BTG.CasePricing.Application.Interfaces;

namespace BTG.CasePricing.Application.Settings;

public class JwtSettings : IAppSettings
{
    public string ValidIssuer { get; set; }
    public string ValidAudience { get; set; }
    public string SecretKey { get; set; }
    public int TokenExpirationInMinutes { get; set; }
    public int RefreshTokenExpirationInDays { get; set; }
}