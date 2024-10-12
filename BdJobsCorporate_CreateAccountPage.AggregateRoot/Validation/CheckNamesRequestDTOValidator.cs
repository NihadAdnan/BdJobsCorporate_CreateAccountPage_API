using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CheckNamesRequestDTOValidator : AbstractValidator<CheckNamesRequestDTO>
{
    public CheckNamesRequestDTOValidator()
    {
        RuleFor(x => x.CheckFor)
            .NotEmpty().WithMessage("CheckFor is required.")
            .Must(x => x == "c" || x == "u").WithMessage("CheckFor must be either 'c' for company or 'u' for username.");

        When(x => x.CheckFor == "u", () =>
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required when checking for a username.")
                .Length(3, 20).WithMessage("UserName must be between 3 and 20 characters.");
        });

        When(x => x.CheckFor == "c", () =>
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("CompanyName is required when checking for a company.");
        });
    }
}
