using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotCadastralMunicipalityProfile : Profile
    {
        public PlotCadastralMunicipalityProfile()
        {
            CreateMap<PlotCadastralMunicipality, PlotCadastralMunicipalityDto>();
            CreateMap<PlotCadastralMunicipalityCreationDto, PlotCadastralMunicipality>();
            CreateMap<PlotCadastralMunicipalityUpdateDto, PlotCadastralMunicipality>();
            CreateMap<PlotCadastralMunicipality, PlotCadastralMunicipality>();
        }
    }
}
