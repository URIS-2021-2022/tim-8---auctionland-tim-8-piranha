using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Profiles
{
    public class DocumentStatusProfile : Profile
    {
        public DocumentStatusProfile()
        {

            CreateMap<DocumentStatus, DocumentStatusDto>();
            CreateMap<DocumentStatus, DocumentStatusConfirmation>();
            CreateMap<DocumentStatusCreationDto, DocumentStatus>();
            CreateMap<DocumentStatusUpdateDto, DocumentStatus>();
            CreateMap<DocumentStatus, DocumentStatus>();
           
        }
    }
}
