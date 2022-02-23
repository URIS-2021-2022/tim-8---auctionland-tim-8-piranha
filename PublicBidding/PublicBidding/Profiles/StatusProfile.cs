using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            CreateMap<Status, StatusDto>();
            CreateMap<Status, Status>();
        }
    }
}
