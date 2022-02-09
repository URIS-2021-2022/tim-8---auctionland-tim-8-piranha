using AutoMapper;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Profiles
{
    public class TypeProfile : Profile
    {
        public TypeProfile()
        {

            CreateMap<Type, TypeDto>();
            CreateMap<Type, Type>();
        }
    }
}
