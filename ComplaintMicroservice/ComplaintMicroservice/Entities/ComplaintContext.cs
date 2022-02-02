using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities
{
    public class ComplaintContext : DbContext
    {
        public ComplaintContext(DbContextOptions<ComplaintContext> options) : base(options)
        {

        }

        public DbSet<ComplaintTypeModel> ComplaintTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComplaintTypeModel>()
                .HasData(new 
                {
                    ComplaintTypeId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ComplaintType = "Zalba na tok javnog nadmetanja"
                });

            modelBuilder.Entity<ComplaintTypeModel>()
                .HasData(new
                {
                    ComplaintTypeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ComplaintType = "Zalba na Odluku o davanju u zakup"
                });
        }
    }
}
