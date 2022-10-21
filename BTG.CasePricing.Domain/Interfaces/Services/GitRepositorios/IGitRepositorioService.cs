using BTG.CasePricing.Domain.Interfaces.DTOs.Requests;
using BTG.CasePricing.Domain.Interfaces.DTOs.Responses;

namespace BTG.CasePricing.Domain.Interfaces.Services.GitRepositorios
{
    public interface IGitRepositorioService
    {
        Task<GitRepositorioResponse> ObterRepositorio(GitRepositorioRequest gitRepositorioRequest);
    }
}