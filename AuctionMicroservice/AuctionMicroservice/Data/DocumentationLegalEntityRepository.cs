using AuctionMicroservice.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
        public async Task<DocumentationLegalEntityConfirmation> CreateDocumentationLegalEntityAsync(DocumentationLegalEntity documentation)
        {
            var documentationEntity = await context.AddAsync(documentation);

            return  mapper.Map<DocumentationLegalEntityConfirmation>(documentationEntity.Entity);
        }

        public async Task DeleteDocumentationAsync(Guid DocumentationLegalEntityId)
        {
            var documentation = await GetDocumentationByIdAsync(DocumentationLegalEntityId);

            context.Remove(documentation);
        }

        public async Task<DocumentationLegalEntity> GetDocumentationByIdAsync(Guid DocumentationLegalEntityId)
        {
            return await context.documentationLegalEntity.FirstOrDefaultAsync(e => e.DocumentationLegalEntityId == DocumentationLegalEntityId);
        }

        public async Task<List<DocumentationLegalEntity>> GetDocumentationLegalEntitesByAuctionAsync(Guid AuctionId)
        {
            return await context.documentationLegalEntity.Where(e => e.AuctionId == AuctionId).ToListAsync();
        }

        public async Task<List<DocumentationLegalEntity>> GetDocumentationLegalEntitiesAsync()
        {
            return await context.documentationLegalEntity.ToListAsync();
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateDocumentationAsync(DocumentationLegalEntity documentation)
        {
           //updates documentation
        }
    }
}
