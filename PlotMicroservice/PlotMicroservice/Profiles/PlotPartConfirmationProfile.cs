using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotPartConfirmationProfile : Profile
    {
        public PlotPartConfirmationProfile()
        {
            CreateMap<PlotPartConfirmation, PlotPartConfirmationDto>();
        }
    }
}
