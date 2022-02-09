using FluentValidation;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Validators
{
    public class PlotPartProtectedZoneValidator : AbstractValidator<PlotPartProtectedZone>
    {
        public PlotPartProtectedZoneValidator()
        {
            RuleFor(plotPartProtectedZone => plotPartProtectedZone.ProtectedZone)
                .NotEmpty()
                .NotNull()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
