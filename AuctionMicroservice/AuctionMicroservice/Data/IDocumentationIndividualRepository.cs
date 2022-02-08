using AuctionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public interface IDocumentationIndividualRepository
    {
        DocumentationIndividualConformation CreateDocumentationIndividual(DocumentationIndividual documentation);

        List<DocumentationIndividual> GetDocumentationIndividuals();

        //By Auctions
        List<DocumentationIndividual> GetDocumentationIndividualsByAuction(Guid AuctionId);

        DocumentationIndividual GetDocumentationById(Guid DocumentationIndividualId);

        void UpdateDocumentation(DocumentationIndividual documentation);

        void DeleteDocumentation(Guid DocumentationIndividualId);

        bool SaveChanges();

    }
}
