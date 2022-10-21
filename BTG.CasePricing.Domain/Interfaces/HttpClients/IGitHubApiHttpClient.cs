using BTG.CasePricing.Domain.Interfaces.DTOs.Requests;
using BTG.CasePricing.Domain.Interfaces.DTOs.Responses;

namespace BTG.CasePricing.Domain.Interfaces.HttpClients
{
    public interface IGitHubApiHttpClient
    {
        Task<GitRepositorioResponse> ObterRepositorio(GitRepositorioRequest gitRepositorioRequest);
    }
}
