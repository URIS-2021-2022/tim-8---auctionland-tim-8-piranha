using FluentValidation;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Validators
{
    public class PlotPartValidator : AbstractValidator<PlotPart>
    {
        public PlotPartValidator()
        {
            RuleFor(plotPart => plotPart.PlotPartNumber)
                .NotEmpty()
                .NotNull()
                .Matches("^[0-9]+(/[0-9]+)*$")
                .Must(plotPartNumber => plotPartNumber.Contains("/"));

            RuleFor(plotPart => plotPart.PlotPartSurfaceArea)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(500)
                .LessThanOrEqualTo(300000);


            RuleFor(plotPart => plotPart.PlotPartCurrentClass)
                .NotEmpty()
                .Matches("^[a - zA - Z0 - 9] *$");

            RuleFor(plotPart => plotPart.PlotPartCurrentProtectedZone)
                .NotEmpty()
                .Matches("^[a - zA - Z0 - 9] *$");

            
        }
    }
}
