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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Auction>()
            //   .HasData(new
            //   {
            //       AuctionId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0"),
            //       AuctionNum = 1,
            //       Year = 2022,
            //       Date = new DateTime(),
            //       Restriction = 25,
            //       PriceStep = 13,
            //       ApplicationDeadline = new DateTime(),
                   

            //   });

            //builder.Entity<Buyer>()
            //    .HasData(new
            //    {
            //        BuyerId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c1"),
            //        BoughtSurface = 23,
            //        RestrictionStart = new DateTime(),
            //        RestrictionPeriodInYears = 12,
            //        RestrictionEnd = new DateTime(),
                  
            //    });


            builder.Entity<Registration>()
                .HasData(new
                {
                    RegistrationId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0"),
                    Date = new DateTime(),
                    Location = "TEst",
                    AuctionId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c0"),
                    BuyerId = Guid.Parse("6a421c13-a195-48f7-8dbd-67596c3974c1")

                });

            //builder.Entity<Registration>()
            //    .HasData(new
            //    {
            //        RegistrationId = "6a422c13-a195-48f7-8dbd-67596c3974c0",
            //        Date = new DateTime(),
            //        Location = "TEst2"

            //    });

           

        }
    }
}
