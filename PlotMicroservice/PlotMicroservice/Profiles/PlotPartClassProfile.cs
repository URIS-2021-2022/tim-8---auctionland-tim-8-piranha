using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartClassModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotPartClassProfile : Profile
    {
        public PlotPartClassProfile()
        {
            CreateMap<PlotPartClass, PlotPartClassConfirmation>();
            CreateMap<PlotPartClass, PlotPartClassDto>();
            CreateMap<PlotPartClassCreationDto, PlotPartClass>();
            CreateMap<PlotPartClassUpdateDto, PlotPartClass>();
            CreateMap<PlotPartClass, PlotPartClass>();
        }
    }
}
