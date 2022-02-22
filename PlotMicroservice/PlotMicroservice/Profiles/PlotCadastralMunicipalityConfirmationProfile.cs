using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotCadastralMunicipalityConfirmationProfile : Profile
    {
        public PlotCadastralMunicipalityConfirmationProfile()
        {
            CreateMap<PlotCadastralMunicipality, PlotCadastralMunicipalityConfirmation>();
            CreateMap<PlotCadastralMunicipalityConfirmation, PlotCadastralMunicipalityConfirmationDto>();
        }
    }
}
