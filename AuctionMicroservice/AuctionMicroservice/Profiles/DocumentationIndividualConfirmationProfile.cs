using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AuctionMicroservice.Entities;
using AuctionMicroservice.Models;

namespace AuctionMicroservice.Profiles
{
    public class DocumentationIndividualConfirmationProfile : Profile
    {
        public DocumentationIndividualConfirmationProfile()
        {
            CreateMap<DocumentationIndividualConformation, DocumentationIndividualConfirmationDto>();
            CreateMap<DocumentationIndividual, DocumentationIndividualConformation>();
        }
    }
}
