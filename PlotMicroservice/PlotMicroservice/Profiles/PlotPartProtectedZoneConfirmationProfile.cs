using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartProtectedZoneModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotPartProtectedZoneConfirmationProfile : Profile
    {
        public PlotPartProtectedZoneConfirmationProfile()
        {
            CreateMap<PlotPartProtectedZoneConfirmation, PlotPartProtectedZoneConfirmationDto>();
        }
    }
}
