using FluentValidation;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Validators
{
    public class PlotCultureValidator : AbstractValidator<PlotCulture>
    {
        public PlotCultureValidator()
        {
            RuleFor(plotCulture => plotCulture.Culture)
                .NotEmpty()
                .NotNull()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
