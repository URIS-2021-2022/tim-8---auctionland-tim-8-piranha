using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotConfirmationProfile : Profile
    {
        public PlotConfirmationProfile()
        {
            CreateMap<PlotConfirmation, PlotConfirmationDto>();
        }
    }
}
