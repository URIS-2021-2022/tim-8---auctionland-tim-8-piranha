using AutoMapper;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.AuthorizedPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Profiles
{
    public class AuthorizedPersonProfile : Profile
    {
        public AuthorizedPersonProfile()
        {
            CreateMap<AuthorizedPersonConfirmation, AuthorizedPersonConfirmationDto>();
            CreateMap<AuthorizedPerson, AuthorizedPersonDto>();
            CreateMap<AuthorizedPersonCreationDto, AuthorizedPerson>();
            CreateMap<AuthorizedPersonUpdateDto, AuthorizedPerson>();
            CreateMap<AuthorizedPerson, AuthorizedPerson>();
            CreateMap<AuthorizedPerson, AuthorizedPersonConfirmation>();
        }
    }
}
