using AutoMapper;
using AddressMicroservice.Entities;
using AddressMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AddressMicroservice.Models.State;

namespace AddressMicroservice.Profiles
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<State, StateDto>();
            CreateMap<StateCreationDto, State>();
            CreateMap<State, StateConfirmation>();
            CreateMap<StateConfirmation, StateConfirmationDto>();
            CreateMap<StateUpdateDto, State>();
            CreateMap<State, State>();
        }
    }
}