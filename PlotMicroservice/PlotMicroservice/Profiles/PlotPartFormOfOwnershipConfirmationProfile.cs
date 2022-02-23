using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartFormOfOwnershipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotPartFormOfOwnershipConfirmationProfile : Profile
    {
        public PlotPartFormOfOwnershipConfirmationProfile()
        {
            CreateMap<PlotPartFormOfOwnershipConfirmation, PlotPartFormOfOwnershipConfirmationDto>();
        }
    }
}
