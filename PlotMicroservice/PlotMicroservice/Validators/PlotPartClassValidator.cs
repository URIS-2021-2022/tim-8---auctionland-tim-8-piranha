using FluentValidation;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Validators
{
    public class PlotPartClassValidator : AbstractValidator<PlotPartClass>
    {
        public PlotPartClassValidator()
        {
            RuleFor(plotPartClass => plotPartClass.Class)
                .NotEmpty()
                .NotNull()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
