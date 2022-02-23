using AutoMapper;
using RegistrationMicroservice.Entities;
using RegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Profiles
{
    public class RegistrationConfirmationProfile : Profile
    {
        public RegistrationConfirmationProfile()
        {
            CreateMap<RegistrationConfirmation, RegistrationConfirmationDto>();
            CreateMap<Registration, RegistrationConfirmation>();
        }
    }
}
