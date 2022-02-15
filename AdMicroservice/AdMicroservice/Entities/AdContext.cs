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

        }


    }
}
