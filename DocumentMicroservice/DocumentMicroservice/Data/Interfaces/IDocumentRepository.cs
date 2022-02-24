

using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Interfaces
{
    public interface IDocumentRepository
    {
        Task<List<Document>> GetDocumentAsync(string rNumber = null, string documentTemplate = null);

        Task<Document> GetDocumentByIdAsync(Guid documentId);

       Task<DocumentConfirmation> CreateDocumentAsync(Document document);

        Task UpdateDocumentAsync(Document document);

        Task DeleteDocumentAsync(Guid documentId);

        Task<bool> SaveChangesAsync();
    }
}
