using AutoMapper;
using ComplaintMicroservice.Entities.Event;
using ComplaintMicroservice.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Profiles.Event
{
    public class ComplaintEventProfile : Profile
    {
        public ComplaintEventProfile()
        {
            CreateMap<ComplaintEvent, ComplaintEventDto>()
                .ForMember(
                    dest => dest.Event,
                    opt => opt.MapFrom(src => $"{src.Event}"));
            CreateMap<ComplaintEventCreationDto, ComplaintEvent>();
            CreateMap<ComplaintEventUpdateDto, ComplaintEvent>();
            CreateMap<ComplaintEvent, ComplaintEvent>();
        }
    }
}
