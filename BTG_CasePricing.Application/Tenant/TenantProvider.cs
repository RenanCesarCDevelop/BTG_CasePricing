using BTG.CasePricing.Domain.Interfaces.Tenant;
using Microsoft.AspNetCore.Http;

namespace BTG.CasePricing.Application.Tenant;

public class TenantProvider : ITenantProvider
{
    private const string Linguagem = "linguagem";

    private List<string> _linguagens;
    private string _linguagem { get; set; }

    public TenantProvider(IHttpContextAccessor accessor)
    {
        if (accessor != null)
        {
            var linguagens = CarregarLinguagem();

            if (linguagens.Any())
            {
                string headerCanalVenda = accessor.HttpContext.Request.Query[Linguagem].ToString()?.ToLower();

                if (!string.IsNullOrEmpty(headerCanalVenda))
                {
                    string tenantCanal = linguagens.FirstOrDefault(x => x.ToLower() == headerCanalVenda);

                    if (tenantCanal != null)
                        _linguagem = tenantCanal;
                }
            }
        }
    }

    public string ObterLinguagens()
        => _linguagem ?? string.Empty;

    private List<string> CarregarLinguagem()
    {
        if (_linguagens != null)
            return _linguagens;

        _linguagens = new List<string>
            {
                "C#",
                "JavaScript",
                "Pyton",
                "Ruby",
                "VB.NET"
            };

        return _linguagens;
    }
}