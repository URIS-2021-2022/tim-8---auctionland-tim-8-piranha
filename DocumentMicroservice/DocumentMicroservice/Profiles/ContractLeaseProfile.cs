using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models.ContractLease;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Profiles
{
    public class ContractLeaseProfile : Profile
    {
        public ContractLeaseProfile()
        {
            CreateMap<ContractLeaseConfirmation, ContractLeaseConfirmationDto>();
            CreateMap<ContractLease, ContractLeaseDto>();
            CreateMap<ContractLeaseCreationDto, ContractLease>();
            CreateMap<ContractLeaseUpdateDto, ContractLease>();
            CreateMap<ContractLease, ContractLease>();
            CreateMap<ContractLease, ContractLeaseConfirmation>();
        }
    }
}
