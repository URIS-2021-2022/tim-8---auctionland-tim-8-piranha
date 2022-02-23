using AutoMapper;
using PersonMicroservice.Entities;
using PersonMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Profiles
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<Board, BoardDto>();
            CreateMap<BoardCreationDto, Board>()
                    .ForMember(
                        dest => dest.Members,
                        opt => opt.Ignore());
            CreateMap<Guid, Person>()
                    .ForMember(
                        dest => dest.PersonId,
                        opt => opt.MapFrom(src => src));
            CreateMap<BoardUpdateDto, Board>()
                .ForMember(
                    dest => dest.Members,
                    opt => opt.Ignore());
            CreateMap<Board, Board>();
            CreateMap<BoardConfirmation, BoardConfirmationDto>();
            CreateMap<Board, BoardConfirmationDto>();
            CreateMap<Board, BoardConfirmation>();
        }
    }
}
