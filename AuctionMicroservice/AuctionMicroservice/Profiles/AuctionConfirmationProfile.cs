using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Profiles
{
    public class AuctionConfirmationProfile : Profile
    {
        public AuctionConfirmationProfile()
        {
            CreateMap<AuctionConfirmation, AuctionConformationDto>();
            CreateMap<Auction, AuctionConfirmation>();
        }
    }
}
