using AutoMapper;
using PersonMicroservice.Entities;
using PersonMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Profiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonConfirmation> ();
            CreateMap<PersonConfirmation, PersonConfirmationDto>();
            CreateMap<PersonCreationDto, Person>();
            CreateMap<PersonUpdateDto, Person>();
            CreateMap<Person, Person>();
            CreateMap<Person, PersonDto>();
        }
    }
}
