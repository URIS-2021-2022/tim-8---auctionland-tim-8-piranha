using DocumentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Data.Interfaces
{
    public interface IDocumentStatusRepository
    {
        Task<List<DocumentStatus>> GetDocumentStatusAsync(string documentStatus = null);

        Task<DocumentStatus> GetDocumentStatusByIdAsync(Guid documentStatusId);

        Task<DocumentStatusConfirmation> CreateDocumentStatusAsync(DocumentStatus documentStatus);

        Task UpdateDocumentStatusAsync(DocumentStatus documentStatus);

        Task DeleteDocumentStatusAsync(Guid DocumentStatusId);

        Task<bool> SaveChangesAsync();
    }
}
