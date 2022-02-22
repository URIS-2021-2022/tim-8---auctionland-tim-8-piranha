using AuctionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public interface IDocumentationLegalEntityRepository
    {
        Task<DocumentationLegalEntityConfirmation> CreateDocumentationLegalEntityAsync(DocumentationLegalEntity documentation);

        Task<List<DocumentationLegalEntity>> GetDocumentationLegalEntitiesAsync();

        //By Auctions
        Task<List<DocumentationLegalEntity>> GetDocumentationLegalEntitesByAuctionAsync(Guid AuctionId);

        Task<DocumentationLegalEntity> GetDocumentationByIdAsync(Guid DocumentationLegalEntityId);

        Task UpdateDocumentationAsync(DocumentationLegalEntity documentation);

        Task DeleteDocumentationAsync(Guid DocumentationLegalEntityId);

        Task<bool> SaveChangesAsync();
    }
}
