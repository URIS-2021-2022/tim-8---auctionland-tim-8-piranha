using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartFormOfOwnershipModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotPartFormOfOwnershipProfile : Profile
    {
        public PlotPartFormOfOwnershipProfile()
        {
            CreateMap<PlotPartFormOfOwnership, PlotPartFormOfOwnershipConfirmation>();
            CreateMap<PlotPartFormOfOwnership, PlotPartFormOfOwnershipDto>();
            CreateMap<PlotPartFormOfOwnershipCreationDto, PlotPartFormOfOwnership>();
            CreateMap<PlotPartFormOfOwnershipUpdateDto, PlotPartFormOfOwnership>();
            CreateMap<PlotPartFormOfOwnership, PlotPartFormOfOwnership>();
        }
    }
}
