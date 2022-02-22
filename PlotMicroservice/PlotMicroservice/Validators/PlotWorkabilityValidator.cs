using FluentValidation;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Validators
{
    public class PlotWorkabilityValidator : AbstractValidator<PlotWorkability>
    {
        public PlotWorkabilityValidator()
        {
            RuleFor(plotWorkability => plotWorkability.Workability)
                .NotEmpty()
                .NotNull()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
