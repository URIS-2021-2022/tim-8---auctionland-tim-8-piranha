using AutoMapper;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DocumentMicroservice.Validators;

namespace DocumentMicroservice.Data.Repositories
{
    public class DocumentStatusRepository : IDocumentStatusRepository
    {
        private readonly DocumentContext Context;
        private readonly IMapper Mapper;
       

        public DocumentStatusRepository(DocumentContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
            
        }

      
        public async Task<DocumentStatusConfirmation> CreateDocumentStatusAsync(DocumentStatus documentStatus)
        {
            var createdEntity = await Context.AddAsync(documentStatus);
            return Mapper.Map<DocumentStatusConfirmation>(createdEntity.Entity);
        }
        public async Task DeleteDocumentStatusAsync(Guid documentStatusId)
        {
            var docStatus = await GetDocumentStatusByIdAsync(documentStatusId);
            Context.Remove(docStatus);
        }

        public async Task<List<DocumentStatus>> GetDocumentStatusAsync(string documentStatus = null)
        {
            return await Context.DocumentStatus.Where(o => o.Status == null || documentStatus == null).ToListAsync();
        }

        public async Task< DocumentStatus> GetDocumentStatusByIdAsync(Guid documentStatusId)
        {
            return await Context.DocumentStatus.FirstOrDefaultAsync(o => o.DocStatusID == documentStatusId);
        }

        public async Task UpdateDocumentStatusAsync(DocumentStatus documentStatus)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
               kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }

        
    }
}
