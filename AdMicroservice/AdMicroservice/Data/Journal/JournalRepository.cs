using AdMicroservice.Entities;
using AdMicroservice.Entities.Journal;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<JournalConfirmation> CreateJournal(JournalModel journal)
        {
            var createdEntity = await context.Journals.AddAsync(journal);
            context.SaveChanges();
            return mapper.Map<JournalConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteJournal(Guid journalId)
        {
            var journal = await GetJournalById(journalId);
            context.Journals.Remove(journal);
            context.SaveChanges();
        }

        public async Task<List<JournalModel>> GetJournals(string journalNumber = null)
        {
            return await context.Journals.Where(e => journalNumber == null || e.JournalNumber == journalNumber).ToListAsync();

        }

        public async Task<JournalModel> GetJournalById(Guid journalId)
        {
            return await context.Journals.FirstOrDefaultAsync(e => e.JournalId == journalId);
        }

#pragma warning disable CS1998
        public async Task UpdateJournal(JournalModel journal)
        {
            //NE TREBA DA SE IMPLEMENTIRA ZBOG EF
            
        }
#pragma warning restore CS1998
    }
}
