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
        private readonly AuctionContext context;
        private readonly IMapper mapper;

        public DocumentationIndividualRepository(AuctionContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public DocumentationIndividualConformation CreateDocumentationIndividual(DocumentationIndividual documentation)
        {
            var documentationEntity = context.Add(documentation);

            return mapper.Map<DocumentationIndividualConformation>(documentationEntity.Entity);
        }

        public void DeleteDocumentation(Guid DocumentationIndividualId)
        {
            var documentation = GetDocumentationById(DocumentationIndividualId);

            context.Remove(documentation);
        }

        public DocumentationIndividual GetDocumentationById(Guid DocumentationIndividualId)
        {
            return context.documentationIndividual.FirstOrDefault(e => e.DocumentationIndividualId == DocumentationIndividualId);
        }

        public List<DocumentationIndividual> GetDocumentationIndividuals()
        {
            return context.documentationIndividual.ToList();
        }

        public List<DocumentationIndividual> GetDocumentationIndividualsByAuction(Guid AuctionId)
        {
            return context.documentationIndividual.Where(e => e.AuctionId == AuctionId).ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateDocumentation(DocumentationIndividual documentation)
        {
            
        }
    }
}
