﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionMicroservice.Models;
using AuctionMicroservice.Entities;
using AutoMapper;

namespace AuctionMicroservice.Profiles
{
    public class DocumentationIndividualProfile : Profile
    {
       public DocumentationIndividualProfile()
        {
            CreateMap<DocumentationIndividual, DocumentationIndividualDto>();//get
            CreateMap<DocumentatonLegalEntitylCreationDto, DocumentationIndividual>();//post
            CreateMap<DocumentationIndividualUpdateDto, DocumentationIndividual>();//update
            CreateMap<DocumentationIndividual, DocumentationIndividual>();


                
        }
    }
}
