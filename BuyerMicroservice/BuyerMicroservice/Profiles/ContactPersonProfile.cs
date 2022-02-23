using AutoMapper;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.ContactPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Profiles
{
    public class ContactPersonProfile : Profile
    {
        public ContactPersonProfile()
        {
            CreateMap<ContactPersonConfirmation, ContactPersonConfirmationDto>();
            CreateMap<ContactPerson, ContactPersonDto>();
            CreateMap<ContactPersonCreationDto, ContactPerson>();
            CreateMap<ContactPersonUpdateDto, ContactPerson>();
            CreateMap<ContactPerson, ContactPerson>();
            CreateMap<ContactPerson, ContactPersonConfirmation>();
        }
    }
}
