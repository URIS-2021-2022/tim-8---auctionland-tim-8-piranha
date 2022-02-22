using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotProfile : Profile
    {
        public PlotProfile()
        {
            CreateMap<Plot, PlotConfirmation>();
            CreateMap<Plot, PlotDto>();
            CreateMap<PlotCreationDto, Plot>();
            CreateMap<PlotUpdateDto, Plot>();
            CreateMap<Plot, Plot>();
        }
    }
}
