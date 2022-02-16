using AdMicroservice.Entities.Ad;
using AdMicroservice.Entities.Journal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdMicroservice.Entities
{
    public class AdContext : DbContext
    {
        public AdContext(DbContextOptions<AdContext> options) : base(options)
        {

        }

        public DbSet<AdModel> Ads { get; set; }
        public DbSet<JournalModel> Journals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //JOURNAL
            modelBuilder.Entity<JournalModel>()
                .HasData(new
                {
                    JournalId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    JournalNumber = "J001",
                    Municipality = "Ruma",
                    DateOfIssue = "12.02.2021."
                });

            modelBuilder.Entity<JournalModel>()
                .HasData(new
                {
                    JournalId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    JournalNumber = "J002",
                    Municipality = "Sremska Mitrovica",
                    DateOfIssue = "22.03.2021."
                });

            //AD    
            modelBuilder.Entity<AdModel>()
                .HasData(new
                {
                    AdId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    PublicationDate = "01.06.2020.",
                    JournalId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0")
                });

            modelBuilder.Entity<AdModel>()
                .HasData(new
                {
                    AdId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    PublicationDate = "01.06.2020.",
                    JournalId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b")
                });
        }


    }
}
