using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Profiles
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<Registration, RegistrationDto>();
            CreateMap<Registration, Registration>();
            CreateMap<RegistrationDto, Registration>();


        }
    }
}
