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
            CreateMap<BuyerUpdateDto, Buyer>();

            CreateMap<Individual, BuyerDto>();
            CreateMap<Individual, IndividualDto>();
            CreateMap<IndividualCreationDto, Individual>();
            CreateMap<Individual, IndividualConfirmation>();
            CreateMap<IndividualConfirmation, IndividualConfirmationDto>();
            CreateMap<IndividualUpdateDto, Individual>().IncludeBase<BuyerUpdateDto,Buyer>();

            CreateMap<LegalEntity, BuyerDto>();
            CreateMap<LegalEntity, LegalEntityDto>();
            CreateMap<LegalEntityCreationDto, LegalEntity>();
            CreateMap<LegalEntity, LegalEntityConfirmation>();
            CreateMap<LegalEntityConfirmation, LegalEntityConfirmationDto>();
            CreateMap<LegalEntityUpdateDto, LegalEntity>().IncludeBase<BuyerUpdateDto, Buyer>() ;

        }
    }
}
