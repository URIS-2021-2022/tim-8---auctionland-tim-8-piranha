using AutoMapper;
using PlotMicroservice.Entities;
using PlotMicroservice.Models.PlotPartProtectedZoneModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlotMicroservice.Profiles
{
    public class PlotPartProtectedZoneProfile : Profile
    {
        public PlotPartProtectedZoneProfile()
        {
            CreateMap<PlotPartProtectedZone, PlotPartProtectedZoneConfirmation>();
            CreateMap<PlotPartProtectedZone, PlotPartProtectedZoneDto>();
            CreateMap<PlotPartProtectedZoneCreationDto, PlotPartProtectedZone>();
            CreateMap<PlotPartProtectedZoneUpdateDto, PlotPartProtectedZone>();
            CreateMap<PlotPartProtectedZone, PlotPartProtectedZone>();
        }
    }
}
