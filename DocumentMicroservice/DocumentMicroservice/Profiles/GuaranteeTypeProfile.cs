using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models.GuaranteeType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Profiles
{
    public class GuaranteeTypeProfile : Profile
    {
        public GuaranteeTypeProfile()
        {
<<<<<<< HEAD
            
=======
            //nema Confirmation!!!
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a
            CreateMap<GuaranteeTypeConfirmation, GuaranteeTypeConfirmationDto>();
            CreateMap<GuaranteeType,GuaranteeTypeDto>();
            CreateMap<GuaranteeTypeCreationDto,GuaranteeType>();
            CreateMap<GuaranteeTypeUpdateDto,GuaranteeType>();
            CreateMap<GuaranteeType, GuaranteeType>();
            CreateMap<GuaranteeType, GuaranteeTypeConfirmation>();
        }
    }
}
