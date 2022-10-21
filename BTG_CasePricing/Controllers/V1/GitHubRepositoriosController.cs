using BTG.CasePricing.Domain.Interfaces.DTOs.Requests;
using BTG.CasePricing.Domain.Interfaces.DTOs.Responses;
using BTG.CasePricing.Domain.Interfaces.Services.GitRepositorios;
using BTG.CasePricing.Domain.Interfaces.Tenant;
using BTG.CasePricing.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace BTG.CasePricing.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class GitHubRepositoriosController : ApiControllerBase
    {
        private readonly IGitRepositorioService _gitRepositorioCommand;
        private readonly ITenantProvider _tenantProvider;

        public GitHubRepositoriosController(IGitRepositorioService gitRepositorioCommand, ITenantProvider tenantProvider)
        {
            _gitRepositorioCommand = gitRepositorioCommand;
            _tenantProvider = tenantProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetRepositorios(string repositorio)
        {
            GitRepositorioResponse result;

            try
            {
                var linguagem = _tenantProvider.ObterLinguagens();

                if (linguagem == null)
                {
                    return RequisicaoIncorreta("Linguagem precisa ser informada no header.");
                }

                var wallet = new GitRepositorioRequest(repositorio, linguagem);
                var validacao = wallet.Validate();

                if (!validacao.IsValid)
                {
                    return RequisicaoIncorreta(string.Join(',', validacao.Errors.Select(x => x.ErrorMessage).ToArray()));
                }

                result = await _gitRepositorioCommand.ObterRepositorio(new GitRepositorioRequest(repositorio, linguagem));

                return Ok(result);
            }
            catch (BadGatewayException ex)
            {
                return ParseExceptionBadGatewayResult(ex);
            }
            catch (Exception ex)
            {
                return ParseExceptionServerErrorResult(ex);
            }
        }
    }
}