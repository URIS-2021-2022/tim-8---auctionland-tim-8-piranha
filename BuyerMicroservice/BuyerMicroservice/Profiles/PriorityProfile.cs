using AutoMapper;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.Priority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Profiles
{
    public class PriorityProfile : Profile
    {
        public PriorityProfile()
        {
            CreateMap<PriorityConfirmation, PriorityConfirmationDto>();
            CreateMap<Priority, PriorityDto>();
            CreateMap<PriorityCreationDto, Priority>();
            CreateMap<PriorityUpdateDto, Priority>();
            CreateMap<Priority, Priority>();
            CreateMap<Priority, PriorityConfirmation>();
        }
    }
}
