using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Entities
{
    //public class DocumentationLegalEntityContext : DbContext
    //{
    //    private readonly IConfiguration configuration;
        
    //    public DocumentationLegalEntityContext(DbContextOptions options, IConfiguration configuration) : base(options)
    //    {
    //        this.configuration = configuration;
    //    }

    //    public DbSet<DocumentationLegalEntity> documentationLegalEntities { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlServer(configuration.GetConnectionString("AuctionDB"));

    //    }

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        builder.Entity<DocumentationLegalEntity>()
    //            .HasData(new
    //            {
    //                DocumentationLegalEntityId = Guid.Parse("6a411c13-a295-48f7-8dbd-67596c3974c0"),
    //                Name = "Goran",
    //                IdentificationNumber  = 17,
    //                Address = "Uzun mirkova 8"

    //            });

    //        builder.Entity<DocumentationLegalEntity>()
    //            .HasData(new
    //            {
    //                DocumentationLegalEntityId = Guid.Parse("6a411c17-a195-48f1-8dbd-67596c3974c0"),
    //                Name = "Stefan",
    //                IdentificationNumber = 3,
    //                Address = "Bulevar 3"
                

    //            });


    //    }




    //}
}
