using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RegistrationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Entities
{
    public class RegistrationContext : DbContext
    {
        private readonly IConfiguration configuration;

        public RegistrationContext(DbContextOptions options, IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Registration> registration { get; set; }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("RegistrationDB"));

        }
        /// <summary>
        /// Inserts seed data into database
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Registration>()
                .HasData(new
                {
                    RegistrationId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0"),
                    Date = new DateTime(),
                    Location = "TEst",
                    AuctionId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0"),
                    BuyerId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c1")

                });


           

        }
    }
}
