using AutoMapper;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Models.ComplaintStatusDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Profiles.Status
{
    public class ComplaintStatusProfile : Profile
    {
        public ComplaintStatusProfile()
        {
            CreateMap<ComplaintStatus, ComplaintStatusDto>()
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => $"{src.Status}"));
            CreateMap<ComplaintStatusCreationDto, ComplaintStatus>();
            CreateMap<ComplaintStatusUpdateDto, ComplaintStatus>();
        }
    }
}
