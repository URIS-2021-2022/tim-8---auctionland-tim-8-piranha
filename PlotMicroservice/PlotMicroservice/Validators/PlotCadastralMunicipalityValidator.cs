using FluentValidation;
using PlotMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Validators
{
    public class PlotCadastralMunicipalityValidator : AbstractValidator<PlotCadastralMunicipality>
    {
        public PlotCadastralMunicipalityValidator()
        {
            RuleFor(plotCadastralMunicipality => plotCadastralMunicipality.CadastralMunicipality)
                    .NotEmpty()
                    .NotNull()
                    .Matches("^[a-zA-Z0-9 ]*$");   
        }
    }
}
