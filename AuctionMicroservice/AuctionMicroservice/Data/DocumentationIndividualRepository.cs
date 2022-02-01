using AuctionMicroservice.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AuctionMicroservice.Data
{
    public class DocumentationIndividualRepository : IDocumentationIndividualRepository
    {
        private readonly DocumentationIndividualContext context;
        private readonly IMapper mapper;

        public DocumentationIndividualRepository(DocumentationIndividualContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public DocumentationIndividualConformation CreateDocumentationIndividual(DocumentationIndividual documentationIndividual)
        {
            var createdEntity = context.Add(documentationIndividual);
            return mapper.Map<DocumentationIndividualConformation>(createdEntity.Entity);
        }

        public void DeleteDocumentationIndividual(Guid DocumentationIndividualId)
        {
            var documentation = GetDocumentationIndividualById(DocumentationIndividualId);
            context.Remove(documentation);
        }

        public DocumentationIndividual GetDocumentationIndividualById(Guid DocumentationIndividualId)
        {
            return context.documentationIndividuals.FirstOrDefault(e => e.DocumentationIndividualId == DocumentationIndividualId);
           
        }


        public List<DocumentationIndividual> GetDocumentationIndividuals(string FirstName = null, string Surname = null, string IdentifiactionNumber = null)
        {
            return context.documentationIndividuals.Where(e => (
            (FirstName == null || e.FirstName == null) &&
            (Surname == null || e.Surname == null) &&
            (IdentifiactionNumber == null || e.IdentificationNumber == null)
            )).ToList();
        }

       

        public void UpdateDocumentationIndividual(DocumentationIndividual documentationIndividual)
        {
            
        }
    }
}
