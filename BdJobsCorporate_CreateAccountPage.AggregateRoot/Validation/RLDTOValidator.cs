﻿using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BdJobsCorporate_CreateAccountPage.AggregateRoot.Validation
{
    public class RLDTOValidator : AbstractValidator<RLNoRequestDTO>
    {
        public RLDTOValidator()
        {
            // RLNo is required and must be numeric
            RuleFor(x => x.RLNo)
                .NotEmpty().WithMessage("RLNo is required.")
                .Must(IsNumeric).WithMessage("RLNo must be numeric.");
        }

        // Helper method to check if the RLNo is numeric
        private bool IsNumeric(string rlNo)
        {
            return int.TryParse(rlNo, out _);
        }
    }
}
