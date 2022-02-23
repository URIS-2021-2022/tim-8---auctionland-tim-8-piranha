using FluentValidation;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Validators
{
    public class PlotPartFormOfOwnershipValidator : AbstractValidator<PlotPartFormOfOwnership>
    {
        public PlotPartFormOfOwnershipValidator()
        {
            RuleFor(plotPartFormOfOwnership => plotPartFormOfOwnership.FormOfOwnership)
                .NotEmpty()
                .NotNull()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
