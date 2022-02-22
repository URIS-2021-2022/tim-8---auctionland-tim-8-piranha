using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotPartProfile : Profile
    {
        public PlotPartProfile()
        {
            CreateMap<PlotPart, PlotPartConfirmation>();
            CreateMap<PlotPart, PlotPartDto>();
            CreateMap<PlotPartCreationDto, PlotPart>();
            CreateMap<PlotPartUpdateDto, PlotPart>();
            CreateMap<PlotPart, PlotPart>();
        }
    }
}
