using BTG.CasePricing.Domain.Result;
using BTG.CasePricing.WebAPI.Extensions;
using BTG.CasePricing.WebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace BTG.CasePricing.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private const string CorpoRequisicaoVazia = "Requisição vazia ou nula";
        protected ApiControllerBase()
        {
        }

        private bool OperacaoValida(BTG.CasePricing.Domain.Result.IResult resultado) => resultado.HasSucceeded;

        protected IActionResult RespostaCustomizada(BTG.CasePricing.Domain.Result.IResult resultado)
        {
            return OperacaoValida(resultado) ? StatusCode(200, resultado) : ParseFailureBadRequestResult(resultado as FailureResult);
        }

        private IActionResult ParseFailureBadRequestResult(FailureResult resultado)
        {
            if (resultado == null)
                return BadRequest();

            return BadRequest(resultado.ToFailureResultModel());
        }

        protected ObjectResult RequisicaoVazia() => StatusCode(StatusCodes.Status400BadRequest, new FailureResultModel(CorpoRequisicaoVazia));
        protected ObjectResult RequisicaoIncorreta(string erro) => StatusCode(StatusCodes.Status400BadRequest, new FailureResultModel(erro));
        protected ObjectResult ParseExceptionServerErrorResult(Exception exception) => StatusCode(StatusCodes.Status500InternalServerError, new FailureResultModel(exception.Message));
        protected ObjectResult ParseExceptionBadGatewayResult(BadGatewayException exception) => StatusCode(StatusCodes.Status502BadGateway, new FailureResultModel(exception.Message));
    }
}
