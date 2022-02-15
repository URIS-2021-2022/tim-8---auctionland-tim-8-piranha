using AdMicroservice.Entities.Journal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data.Journal
{
    public interface IJournalRepository
    {
        List<JournalModel> GetJournals(string journalNumber = null);

        JournalModel GetJournalById(Guid journalId);

        JournalConfirmation CreateJournal(JournalModel journal);

        JournalConfirmation UpdateJournal(JournalModel journal);

        void DeleteJournal(Guid journalId);

    }
}
