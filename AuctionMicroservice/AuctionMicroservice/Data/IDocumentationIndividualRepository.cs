using AuctionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public interface IDocumentationIndividualRepository
    {
        Task<DocumentationIndividualConformation> CreateDocumentationIndividualAsync(DocumentationIndividual documentation);

        Task<List<DocumentationIndividual>> GetDocumentationIndividualsAsync();

        //By Auctions
        Task<List<DocumentationIndividual>> GetDocumentationIndividualsByAuctionAsync(Guid AuctionId);

        Task<DocumentationIndividual> GetDocumentationByIdAsync(Guid DocumentationIndividualId);

        Task UpdateDocumentationAsync(DocumentationIndividual documentation);

        Task DeleteDocumentationAsync(Guid DocumentationIndividualId);

        Task<bool> SaveChangesAsync();

    }
}
