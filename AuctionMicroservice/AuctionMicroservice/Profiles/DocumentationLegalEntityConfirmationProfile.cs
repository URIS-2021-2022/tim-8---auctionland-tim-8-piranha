using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Profiles
{
    public class DocumentationLegalEntityConfirmationProfile : Profile
    {
        public DocumentationLegalEntityConfirmationProfile()
        {
            CreateMap<DocumentationLegalEntityConfirmation, DocumentationLegalEntityConfirmationDto>();
            CreateMap<DocumentationLegalEntity, DocumentationLegalEntityConfirmation>();
        }
    }
}
