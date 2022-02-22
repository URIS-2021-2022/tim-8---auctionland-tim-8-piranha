using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Profiles
{
    public class ComplaintTypeProfile : Profile
    {
        public ComplaintTypeProfile()
        {
            CreateMap<ComplaintTypeModel, ComplaintTypeDto>()
                .ForMember(
                    dest => dest.ComplaintType,
                    opt => opt.MapFrom(src => $"{src.ComplaintType}"));
            CreateMap<ComplaintTypeCreationDto, ComplaintTypeModel>();
            CreateMap<ComplaintTypeUpdateDto, ComplaintTypeModel>();
            CreateMap<ComplaintTypeModel, ComplaintTypeModel>();
        }
    }
}
