using AutoMapper;
using ComplaintMicroservice.Entities.Event;
using ComplaintMicroservice.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Profiles.Event
{
    public class ComplaintEventConfirmationProfile : Profile
    {
        public ComplaintEventConfirmationProfile()
        {
            CreateMap<ComplaintEventConfirmation, ComplaintEventConfirmationDto>()
                .ForMember(
                    dest => dest.Event,
                    opt => opt.MapFrom(src => $"{src.Event}"));
            CreateMap<ComplaintEvent, ComplaintEventConfirmation>();
        }
    }
}
