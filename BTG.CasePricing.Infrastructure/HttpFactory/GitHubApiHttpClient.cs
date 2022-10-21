using BTG.CasePricing.Domain.Constants;
using BTG.CasePricing.Domain.Interfaces.DTOs.Requests;
using BTG.CasePricing.Domain.Interfaces.DTOs.Responses;
using BTG.CasePricing.Domain.Interfaces.HttpClients;
using BTG.CasePricing.Infrastructure.HttpFactory.Abstractions;
using Serilog;

namespace BTG.CasePricing.Infrastructure.HttpFactory
{
    public class GitHubApiHttpClient : IGitHubApiHttpClient
    {
        private readonly IExternalGitHubApi _externalGitHubApi;

        public GitHubApiHttpClient(IExternalGitHubApi externalGitHubApi)
        {
            _externalGitHubApi = externalGitHubApi;
        }

        public async Task<GitRepositorioResponse> ObterRepositorio(GitRepositorioRequest gitRepositorioRequest)
        {

            var headers = new Dictionary<string, string>()
            {
                { "Content-Type", EnvironmentVariablesConstants.ContentType },
                { "User-Agent", "PostmanRuntime/7.29.2" },
            };

            string q = gitRepositorioRequest.Repositorio;
            string language = gitRepositorioRequest.Linguagem;
            var result = await _externalGitHubApi.ObterRepositorios(headers, q, language);

            Log.Information("Response Integração GitHub {@Result}", result);

            return result;
        }
    }
}