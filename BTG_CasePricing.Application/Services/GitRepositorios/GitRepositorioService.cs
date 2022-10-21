using BTG.CasePricing.Application.Exceptions;
using BTG.CasePricing.Domain.Interfaces.DTOs.Requests;
using BTG.CasePricing.Domain.Interfaces.DTOs.Responses;
using BTG.CasePricing.Domain.Interfaces.HttpClients;
using BTG.CasePricing.Domain.Interfaces.Services.GitRepositorios;
using Serilog;

namespace BTG.CasePricing.Application.Commands.GitRepositorios
{
    public class GitRepositorioService : IGitRepositorioService
    {

        private readonly IGitHubApiHttpClient _gitHubApiHttpClient;

        public GitRepositorioService(IGitHubApiHttpClient gitHubApiHttpClient)
        {
            _gitHubApiHttpClient = gitHubApiHttpClient;
        }

        public async Task<GitRepositorioResponse> ObterRepositorio(GitRepositorioRequest gitRepositorioRequest)
        {
            try
            {
                #region Grava Log API - Git Repositorio Request

                Log.Information("GitHub Repositorios Request:{0}", gitRepositorioRequest);

                #endregion

                var repositorios = await _gitHubApiHttpClient.ObterRepositorio(gitRepositorioRequest);

                #region Grava Log API - Wallet Response

                Log.Information("GitHub Repositorios Response:{@Result}", repositorios);

                #endregion

                return repositorios;
            }
            catch (Refit.ApiException ex)
            {
                throw new BadRequestException(ex.Content);
            }
        }
    }
}