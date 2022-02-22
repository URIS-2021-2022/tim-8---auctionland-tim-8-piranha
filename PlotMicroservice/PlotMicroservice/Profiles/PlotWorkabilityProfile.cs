using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotWorkabilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotWorkabilityProfile : Profile
    {
        public PlotWorkabilityProfile()
        {
            CreateMap<PlotWorkability, PlotWorkabilityConfirmation>();
            CreateMap<PlotWorkability, PlotWorkabilityDto>();
            CreateMap<PlotWorkabilityCreationDto, PlotWorkability>();
            CreateMap<PlotWorkabilityUpdateDto, PlotWorkability>();
            CreateMap<PlotWorkability, PlotWorkability>();
        }
    }
}
