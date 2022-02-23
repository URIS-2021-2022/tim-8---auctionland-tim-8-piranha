using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Models.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentConfirmation, DocumentConfirmationDto>();
            CreateMap<Document,DocumentDto>();
            CreateMap<DocumentCreationDto,Document>();
            CreateMap<DocumentUpdateDto,Document>();
            CreateMap<Document,Document>();
            CreateMap<Document, DocumentConfirmation>();

            

        }
    }
}
