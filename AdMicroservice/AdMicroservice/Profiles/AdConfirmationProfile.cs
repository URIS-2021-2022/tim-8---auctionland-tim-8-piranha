using AdMicroservice.Entities.Ad;
using AdMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Profiles
{
    public class AdConfirmationProfile : Profile
    {
        public AdConfirmationProfile()
        {
            CreateMap<AdConfirmation, AdConfirmationDto>()
                .ForMember(
                    dest => dest.PublicationDate,
                    opt => opt.MapFrom(src => $"{src.PublicationDate}"));
            CreateMap<AdModel, AdConfirmation>();
        }
    }
}
