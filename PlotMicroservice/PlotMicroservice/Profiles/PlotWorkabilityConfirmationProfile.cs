using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotWorkabilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotWorkabilityConfirmationProfile : Profile
    {
        public PlotWorkabilityConfirmationProfile()
        {
            CreateMap<PlotWorkabilityConfirmation, PlotWorkabilityConfirmationDto>();   
        }
    }
}
