using AuctionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public interface IDocumentationLegalEntityRepository
    {
        DocumentationLegalEntityConfirmation CreateDocumentationLegalEntity(DocumentationLegalEntity documentation);

        List<DocumentationLegalEntity> GetDocumentationLegalEntities();

        //By Auctions
        List<DocumentationLegalEntity> GetDocumentationLegalEntitesByAuction(Guid AuctionId);

        DocumentationLegalEntity GetDocumentationById(Guid DocumentationLegalEntityId);

        void UpdateDocumentation(DocumentationLegalEntity documentation);

        void DeleteDocumentation(Guid DocumentationLegalEntityId);

        bool SaveChanges();
    }
}
