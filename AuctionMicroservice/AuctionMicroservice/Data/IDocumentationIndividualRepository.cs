using AuctionMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Data
{
    public interface IDocumentationIndividualRepository 
    {
        List<DocumentationIndividual> GetDocumentationIndividuals(string FirstName = null, string Surname = null, string IdentifiactionNumber = null);//zbog query parametra

        DocumentationIndividual GetDocumentationIndividualById(Guid DocumentationIndividualId);

        DocumentationIndividualConformation CreateDocumentationIndividual(DocumentationIndividual documentationIndividual);

        void UpdateDocumentationIndividual(DocumentationIndividual documentationIndividual);

        void DeleteDocumentationIndividual(Guid DocumentationIndividualId);

        bool SaveChanges();


    }
}
