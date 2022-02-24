using AdMicroservice.Entities.Journal;
using AdMicroservice.Models.Journal;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Profiles
{
    public class JournalConfirmationProfile : Profile
    {
        public JournalConfirmationProfile()
        {
            CreateMap<JournalConfirmation, JournalConfirmationDto>()
                .ForMember(
                    dest => dest.JournalNumber,
                    opt => opt.MapFrom(src => $"{src.JournalNumber}"))
                .ForMember(
                    dest => dest.Municipality,
                    opt => opt.MapFrom(src => $"{src.Municipality}"))
                .ForMember(
                    dest => dest.DateOfIssue,
                    opt => opt.MapFrom(src => $"{src.DateOfIssue}")); 
            CreateMap<JournalModel, JournalConfirmation>();
        }
    }
}
