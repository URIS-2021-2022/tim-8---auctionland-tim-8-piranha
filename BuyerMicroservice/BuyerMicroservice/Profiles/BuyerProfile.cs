using AutoMapper;
using BuyerMicroservice.Entities;
using BuyerMicroservice.Models.Buyer;
using BuyerMicroservice.Models.Individual;
using BuyerMicroservice.Models.LegalEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Profiles
{
    public class BuyerProfile : Profile
    {
        public BuyerProfile()
        {
            CreateMap<IndividualCreationDto, Individual>();
            CreateMap<LegalEntityCreationDto, LegalEntity>();
            CreateMap<Buyer, BuyerConfirmation>();
            CreateMap<Individual, BuyerDto>();
            CreateMap<LegalEntity, BuyerDto>();
            CreateMap<BuyerConfirmation,BuyerConfirmationDto>();
            
        }
    }
}
