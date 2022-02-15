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

        public DbSet<PublicBiddingDto> publicBiddingDto { get; set; }

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
            //builder.Entity<Auction>().HasMany(d => d.DocumentationIndividual).WithOne();
            // builder.Entity<DocumentationIndividual>().HasOne().WithMany(d => d.DocumentationIndividual);
            //builder.Entity<Auction>().HasMany(d => d.DocumentationLegalEntity).WithOne(a => a.Auction);
            //builder.Entity<Auction>().HasMany(p => p.PublicBiddings).WithOne();






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

            //    builder.Entity<Auction>()
            //.HasData(new
            //{
            //    AuctionId = Guid.Parse("6a321c13-a195-42f7-8dbd-67596c3974c0"),
            //    AuctionNum = 2,
            //    Year = 2021,
            //    Date = new DateTime(),
            //    Restriction = 22,
            //    PriceStep = 17,
            //    DocumentationIndividual = new DocumentationIndividual { DocumentationIndividualId = Guid.Parse("6a411c13-a195-18f7-8dbd-67596c6974c0"), FirstName = "Test", Surname = "Maric", IdentificationNumber = "1324666", AuctionId = Guid.Parse("6a321c13-a195-42f7-8dbd-67596c3974c0") },



            //    DocumentationLegalEntity = new DocumentationLegalEntity { DocumentationLegalEntityId = Guid.Parse("6a411c13-a195-18f7-8dbd-67536c3924c0"), Name = "TestSubjec33", IdentificationNumber = "1121513", Address = "Milivoja 123", AuctionId = Guid.Parse("6a321c13-a195-42f7-8dbd-67596c3974c0") },


            //    PublicBiddings = new PublicBiddingDto
            //    {
            //        PublicBiddingId = Guid.Parse("6a44e733-a195-18f7-8dbd-67536c3924c3"),
            //        Date = new DateTime(),
            //        StartTime = new DateTime(),
            //        EndTime = new DateTime(),
            //        BegginingPriceByHectare = 15,
            //        Skipped = true,
            //        AuctionedPrice = 150,
            //        LeasePeriod = 2,
            //        ContestantsNumber = 20,
            //        DepositAdditionPrice = 120,
            //        Round = 1,
            //        AuctionId = Guid.Parse("6a321c13-a195-42f7-8dbd-67596c3974c0")
            //    },

            //    ApplicationDeadline = new DateTime()


            //        });

            //builder.Entity<DocumentationIndividual>()
            //    .HasData(new
            //    {
            //        DocumentationIndividualId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
            //        FirstName = "Marko",
            //        Surname = "Milic",
            //        IdentificationNumber = "0819423841941"


            //});

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

            //builder.Entity<DocumentationLegalEntity>()
            //    .HasData(new
            //    {
            //        DocumentationLegalEntityId = Guid.Parse("6a411c17-a195-48f1-8dbd-67596c3974c0"),
            //        Name = "Stefan",
            //        IdentificationNumber = "118",
            //        Address = "Bulevar 3",
            //        AuctionId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0")


            //    });

            builder.Entity<PublicBiddingDto>()
                .HasData(
                new PublicBiddingDto
                {
                    PublicBiddingId = Guid.Parse("6a411c13-a295-18f7-8dbd-67536c3924c3"),
                    Date = new DateTime(),
                    StartTime = new DateTime(),
                    EndTime = new DateTime(),
                    BegginingPriceByHectare = 13,
                    Skipped = false,
                    AuctionedPrice = 250,
                    LeasePeriod = 3,
                    ContestantsNumber = 21,
                    DepositAdditionPrice = 123,
                    Round = 2,
                    AuctionId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0")
                }
                );




        }
    }
}
