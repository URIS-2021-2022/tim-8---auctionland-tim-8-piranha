using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    public class DocumentationIndividualContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DocumentationIndividualContext(DbContextOptions  options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<DocumentationIndividual> documentationIndividuals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AuctionDB"));

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DocumentationIndividual>()
                .HasData(new
                {
                    DocumentationIndividualId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    FirstName = "Marko",
                    Surname = "Milic",
                    IdentificationNumber = "0819423841941"

                });

            builder.Entity<DocumentationIndividual>()
                .HasData(new
                {
                    DocumentationIndividualId = Guid.Parse("6a411c17-a195-48f7-8dbd-67596c3974c0"),
                    FirstName = "Stefan",
                    Surname = "Zoric",
                    IdentificationNumber = "0214120948120"

                });


        }

    }
}
