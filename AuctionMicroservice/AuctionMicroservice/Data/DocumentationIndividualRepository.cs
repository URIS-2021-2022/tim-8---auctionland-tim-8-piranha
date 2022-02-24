using AuctionMicroservice.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<DocumentationIndividualConformation> CreateDocumentationIndividualAsync(DocumentationIndividual documentation)
        {
            var documentationEntity = await context.AddAsync(documentation);

            return mapper.Map<DocumentationIndividualConformation>(documentationEntity.Entity);
        }

        public async Task DeleteDocumentationAsync(Guid DocumentationIndividualId)
        {
            var documentation = await GetDocumentationByIdAsync(DocumentationIndividualId);

            context.Remove(documentation);
        }

        public async Task<DocumentationIndividual> GetDocumentationByIdAsync(Guid DocumentationIndividualId)
        {
            return await context.documentationIndividual.FirstOrDefaultAsync(e => e.DocumentationIndividualId == DocumentationIndividualId);
        }

        public async Task<List<DocumentationIndividual>> GetDocumentationIndividualsAsync()
        {
            return await context.documentationIndividual.ToListAsync();
        }

        public async Task<List<DocumentationIndividual>> GetDocumentationIndividualsByAuctionAsync(Guid AuctionId)
        {
            return await context.documentationIndividual.Where(e => e.AuctionId == AuctionId).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateDocumentationAsync(DocumentationIndividual documentation)
        {
            //updates documentation
        }
    }
}
