using BTG.CasePricing.Domain.Validators;
using FluentValidation.Results;

namespace BTG.CasePricing.Domain.Interfaces.DTOs.Requests
{
    public class GitRepositorioRequest
    {
        public GitRepositorioRequest(string repositorio, string linguagem)
        {
            Repositorio = repositorio;
            Linguagem = linguagem;
        }

        public string Repositorio { get; set; }

        public string Linguagem { get; set; }

        public ValidationResult Validate()
        {
            var validator = new GitRepositorioValidator();

            return validator.Validate(this);
        }
    }
}
