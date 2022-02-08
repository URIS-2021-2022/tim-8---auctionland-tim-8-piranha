using AuctionMicroservice.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public class DocumentationLegalEntityRepository : IDocumentationLegalEntityRepository
    {
        private readonly AuctionContext context;
        private readonly IMapper mapper;

        public DocumentationLegalEntityRepository(AuctionContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public DocumentationLegalEntityConfirmation CreateDocumentationLegalEntity(DocumentationLegalEntity documentation)
        {
            var documentationEntity = context.Add(documentation);

            return mapper.Map<DocumentationLegalEntityConfirmation>(documentationEntity.Entity);
        }

        public void DeleteDocumentation(Guid DocumentationLegalEntityId)
        {
            var documentation = GetDocumentationById(DocumentationLegalEntityId);

            context.Remove(documentation);
        }

        public DocumentationLegalEntity GetDocumentationById(Guid DocumentationLegalEntityId)
        {
            return context.documentationLegalEntity.FirstOrDefault(e => e.DocumentationLegalEntityId == DocumentationLegalEntityId);
        }

        public List<DocumentationLegalEntity> GetDocumentationLegalEntitesByAuction(Guid AuctionId)
        {
            return context.documentationLegalEntity.Where(e => e.AuctionId == AuctionId).ToList();
        }

        public List<DocumentationLegalEntity> GetDocumentationLegalEntities()
        {
            return context.documentationLegalEntity.ToList();
        }


        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateDocumentation(DocumentationLegalEntity documentation)
        {
           
        }
    }
}
