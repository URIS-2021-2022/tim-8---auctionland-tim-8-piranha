using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartClassModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotPartClassConfirmationProfile : Profile
    {
        public PlotPartClassConfirmationProfile()
        {
            CreateMap<PlotPartClassConfirmation, PlotPartClassConfirmationDto>();
        }
    }
}
