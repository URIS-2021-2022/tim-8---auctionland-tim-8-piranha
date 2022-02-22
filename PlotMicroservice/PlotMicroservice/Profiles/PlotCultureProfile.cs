using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotCultureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotCultureProfile : Profile
    {
        public PlotCultureProfile()
        {
            CreateMap<PlotCulture, PlotCultureConfirmation>();
            CreateMap<PlotCulture, PlotCultureDto>();
            CreateMap<PlotCultureCreationDto, PlotCulture>();
            CreateMap<PlotCultureUpdateDto, PlotCulture>();
            CreateMap<PlotCulture, PlotCulture>();
        }
    }
}
