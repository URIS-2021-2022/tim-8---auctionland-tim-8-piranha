using AutoMapper;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.BoardNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Profiles
{
    public class BoardNumberProfile : Profile
    {

        public BoardNumberProfile()
        {
            CreateMap<BoardNumberConfirmation, BoardNumberConfirmationDto>();
            CreateMap<BoardNumber, BoardNumberDto>();
            CreateMap<BoardNumberCreationDto, BoardNumber>();
            CreateMap<BoardNumberUpdateDto, BoardNumber>();
            CreateMap<BoardNumber, BoardNumber>();
            CreateMap<BoardNumber, BoardNumberConfirmation>();
        }
     
    }
}
