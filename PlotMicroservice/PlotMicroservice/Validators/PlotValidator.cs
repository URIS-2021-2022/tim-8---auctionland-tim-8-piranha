using FluentValidation;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Validators
{
    public class PlotValidator : AbstractValidator<Plot>
    {
        public PlotValidator()
        {
            RuleFor(plot => plot.PlotSurfaceArea)
                .NotEmpty()
                .NotNull()
                .InclusiveBetween(500, 300000);

            RuleFor(plot => plot.PlotNumber)
                .NotEmpty()
                .NotNull()
                .Matches("^(0|[1-9][0-9]*)$");

            RuleFor(plot => plot.PlotRealEstateListNumber)
                .NotEmpty()
                .NotNull()
                .Matches("^[a - zA - Z0 - 9] *$")
                .Must(plotRealEstateListNumber => plotRealEstateListNumber.Contains("LN"));

            RuleFor(plot => plot.PlotCurrentCulture)
                .NotEmpty()
                .Matches("^[a - zA - Z0 - 9] *$");

            RuleFor(plot => plot.PlotCurrentWorkability)
                .NotEmpty()
                .Matches("^[a - zA - Z0 - 9] *$");
        }
    }
}
