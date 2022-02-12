using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Profiles
{
    public class PublicBiddingConfirmationProfile : Profile
    {
        public PublicBiddingConfirmationProfile()
        {
            CreateMap<PublicBiddingConfirmation, PublicBiddingConfirmationDto>();
            CreateMap<Entities.PublicBidding, PublicBiddingConfirmationProfile>();
        }
    }
}
