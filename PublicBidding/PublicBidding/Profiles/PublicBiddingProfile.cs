using AutoMapper;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Profiles
{
    public class PublicBiddingProfile : Profile
    {
        public PublicBiddingProfile()
        {
            CreateMap<Entities.PublicBidding, PublicBiddingDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.StatusName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.TypeName))
                .ForMember(dest => dest.AuthorizedPersons, opt => opt.Ignore())
                .ForMember(dest => dest.BestBidder, opt => opt.Ignore())
                .ForMember(dest => dest.Bidders, opt => opt.Ignore())
                .ForMember(dest => dest.PlotParts, opt => opt.Ignore());
            CreateMap<PublicBiddingCreationDto, Entities.PublicBidding>();
            CreateMap<PublicBiddingUpdateDto, Entities.PublicBidding>();
            CreateMap<Entities.PublicBidding, Entities.PublicBidding>();
            CreateMap<Entities.PublicBidding, PublicBiddingForOtherServices>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.StatusName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.TypeName));
        }
    }
}
