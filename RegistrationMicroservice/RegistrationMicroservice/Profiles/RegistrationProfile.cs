using AutoMapper;
using RegistrationMicroservice.Entities;
using RegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Profiles
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<Registration, RegistrationDto>();
            CreateMap<Registration, Registration>();
            CreateMap<RegistrationCreateDto, Registration>();
            CreateMap<RegistrationUpdateDto, Registration>();
        }

    }
}
