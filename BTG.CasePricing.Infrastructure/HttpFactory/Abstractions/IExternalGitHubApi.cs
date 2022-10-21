using BTG.CasePricing.Domain.Interfaces.DTOs.Responses;
using Refit;

namespace BTG.CasePricing.Infrastructure.HttpFactory.Abstractions
{
    public interface IExternalGitHubApi
    {
        [Get("/search/repositories?q={q}+language:{language}")]
        Task<GitRepositorioResponse> ObterRepositorios([HeaderCollection] IDictionary<string, string> headers, string q, string language);

    }
}
