using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Profiles
{
    public class DocumentationLegalEntityProfile : Profile
    {

        public DocumentationLegalEntityProfile()
        {
            CreateMap<DocumentationLegalEntityCreationDto, DocumentationLegalEntity>();//post
            CreateMap<DocumentationLegalEntity, DocumentationLegalEntityDto>();//get
            CreateMap<DocumentationLegalEntityUpdateDto, DocumentationLegalEntity>();//update
            CreateMap<DocumentationLegalEntity, DocumentationLegalEntity>();
            


        }
    }
}
