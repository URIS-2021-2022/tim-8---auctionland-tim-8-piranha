using AdMicroservice.Entities.Ad;
using AdMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Profiles
{
    public class AdProfile : Profile
    {
        public AdProfile()
        {
            CreateMap<AdModel, AdDto>()
                .ForMember(
                    dest => dest.PublicationDate,
                    opt => opt.MapFrom(src => $"{src.PublicationDate}"));
            CreateMap<AdCreationDto, AdModel>();
            CreateMap<AdUpdateDto, AdModel>();
            CreateMap<AdModel, AdModel>();
        }
    }
}
