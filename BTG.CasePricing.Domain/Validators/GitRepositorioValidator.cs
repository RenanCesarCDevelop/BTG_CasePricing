using BTG.CasePricing.Domain.Interfaces.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.CasePricing.Domain.Validators;

public class GitRepositorioValidator : AbstractValidator<GitRepositorioRequest>
{

    public GitRepositorioValidator()
    {
        RuleFor(x => x.Repositorio)
            .NotEmpty()          
            .WithMessage("Repositorio deve ser preenchido.");

        RuleFor(x => x.Linguagem)
         .NotEmpty()
         .WithMessage("Linguagem deve ser preenchido.");
    }

}

