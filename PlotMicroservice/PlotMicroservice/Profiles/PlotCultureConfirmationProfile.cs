using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotCultureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotCultureConfirmationProfile : Profile
    {
        public PlotCultureConfirmationProfile()
        {
            CreateMap<PlotCultureConfirmation, PlotCultureConfirmationDto>();
        }
    }
}
