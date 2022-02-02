using AutoMapper;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Models.ComplaintStatusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Profiles.Status
{
    public class ComplaintStatusConfirmationProfile : Profile
    {
        public ComplaintStatusConfirmationProfile()
        {
            CreateMap<ComplaintStatusConfirmation, ComplaintStatusConfirmationDto>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => $"{src.Status}"));
        }
    }
}
