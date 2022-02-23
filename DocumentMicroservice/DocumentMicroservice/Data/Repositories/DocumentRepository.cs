using AutoMapper;
using DocumentMicroservice.Data.Interfaces;
using DocumentMicroservice.Entities;
using DocumentMicroservice.Entities.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentContext Context;
        private readonly IMapper Mapper;

        public DocumentRepository(DocumentContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
       
        public async Task<DocumentConfirmation> CreateDocumentAsync(Document document)
        {
            var createdEntity = await Context.AddAsync(document);
            return Mapper.Map<DocumentConfirmation>(createdEntity.Entity);
        }


        public async Task DeleteDocumentAsync(Guid documentId)
        {
            var document =await  GetDocumentByIdAsync(documentId);
            Context.Remove(document);
        }

        public async  Task<List<Document>> GetDocumentAsync(string rNumber = null, string documentTemplate = null)
        {
            return await Context.Document.Where(o => (o.registrationNumber == null || rNumber == null) && (o.documentTemplate == null || documentTemplate == null)).ToListAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(Guid documentId)
        {
            return await Context.Document.FirstOrDefaultAsync(o => o.documentId == documentId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }


        public async Task UpdateDocumentAsync(Document document)
        {
            /* Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze 
               kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane */
        }
    }
}
