using AdMicroservice.Entities.Journal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data.Journal
{
    public interface IJournalRepository
    {
        Task<List<JournalModel>> GetJournals(string journalNumber = null);

        Task<JournalModel> GetJournalById(Guid journalId);

        Task <JournalConfirmation> CreateJournal(JournalModel journal);

        Task UpdateJournal(JournalModel journal);

        Task DeleteJournal(Guid journalId);

        Task<bool> SaveChanges();

    }
}
