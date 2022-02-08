﻿using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Profiles
{
    public class AuctionProfile : Profile
    { 
        public AuctionProfile()
        {
            
            CreateMap<Auction, AuctionDto>();//get
            CreateMap<AuctionCreationDto, Auction>();//post
            CreateMap<AuctionUpdateDto, Auction>();//put
            CreateMap<Auction, Auction>();


        }
    }
}
