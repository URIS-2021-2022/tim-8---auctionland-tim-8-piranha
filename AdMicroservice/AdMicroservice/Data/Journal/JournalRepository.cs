using AdMicroservice.Entities;
using AdMicroservice.Entities.Journal;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Data.Journal
{
    public class JournalRepository : IJournalRepository
    {
        private readonly AdContext context;
        private readonly IMapper mapper;

        public JournalRepository(AdContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public JournalConfirmation CreateJournal(JournalModel journal)
        {
            var createdEntity = context.Journals.Add(journal);
            context.SaveChanges();
            return mapper.Map<JournalConfirmation>(createdEntity.Entity);
        }

        public void DeleteJournal(Guid journalId)
        {
            var journal = GetJournalById(journalId);
            context.Journals.Remove(journal);
            context.SaveChanges();
        }

        public List<JournalModel> GetJournals(string journalNumber = null)
        {
            return context.Journals.Where(e => journalNumber == null || e.JournalNumber == journalNumber).ToList();

        }

        public JournalModel GetJournalById(Guid journalId)
        {
            return context.Journals.FirstOrDefault(e => e.JournalId == journalId);
        }

        public JournalConfirmation UpdateJournal(JournalModel journal)
        {
            //NE GLEDAJ OVAJ KOD   
            JournalModel jr = GetJournalById(journal.JournalId);

            jr.JournalId = journal.JournalId;
            jr.JournalNumber = journal.JournalNumber;

            return new JournalConfirmation
            {
                JournalId = jr.JournalId,
                JournalNumber = jr.JournalNumber
            };
        }
    }
}
