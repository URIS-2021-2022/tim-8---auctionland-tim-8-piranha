using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    public class RegistrationContext : DbContext
    {
        private readonly IConfiguration configuration;

        public RegistrationContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Registration> registration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserDB"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>()
                .HasData(new
                {
                    RegistrationId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c1"),
                    Email = "lukap181@gmail.com",
                    FirstName = "Milan",
                    Surname = "Miki",
                    Password = "12345"
                }); 
        }
    }
}
