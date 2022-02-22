using AuctionMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    public class AuctionContext : DbContext
    {
        private readonly IConfiguration configuration;
        public AuctionContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Auction> auction { get; set; }
        public DbSet<DocumentationIndividual> documentationIndividual { get; set; }

        public DbSet<DocumentationLegalEntity> documentationLegalEntity { get; set; }

        public DbSet<AuctionPublicBidding> auctionPublicBidding { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AuctionDB"));

        }

        /// <summary>
        /// Inserts seed data into database
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            


            builder.Entity<AuctionPublicBidding>()
                .HasOne(p => p.auction)
                .WithMany()
                .HasForeignKey("AuctionId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();



            builder.Entity<Auction>()

                .HasData(new
                {
                    AuctionId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0"),
                    AuctionNum = 1,
                    Year = 2022,
                    Date = new DateTime(),
                    Restriction = 25,
                    PriceStep = 13,
                    ApplicationDeadline = new DateTime()

                });

            

        builder.Entity<DocumentationIndividual>()
                .HasData(new
        {
            DocumentationIndividualId = Guid.Parse("6a411a17-a195-48f7-8dbd-67596c3974c0"),
            FirstName = "Stefan",
            Surname = "Zoric",
            IdentificationNumber = "0214120948120",
            AuctionId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0")


        });

         builder.Entity<DocumentationLegalEntity>()
                .HasData(new
                {
                    DocumentationLegalEntityId = Guid.Parse("6a411c13-a295-48f7-8dbd-67596c3974c0"),
                    Name = "Goran",
                    IdentificationNumber = "17",
                    Address = "Uzun mirkova 8",
                    AuctionId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0")

         });

            


        }
    }
}
