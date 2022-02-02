using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Profiles
{
    public class ComplaintTypeConfirmationProfile : Profile
    {
        public ComplaintTypeConfirmationProfile()
        {
            CreateMap<ComplaintTypeConfirmation, ComplaintTypeConfirmationDto>()
                .ForMember(
                    dest => dest.ComplaintType,
                    opt => opt.MapFrom(src => $"{src.ComplaintType}"));
        }
    }
}
