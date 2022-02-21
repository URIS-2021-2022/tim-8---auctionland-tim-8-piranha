using Microsoft.EntityFrameworkCore;
using System;



namespace DocumentMicroservice.Entities.Contexts
{
    public class DocumentContext : DbContext
    {
        // Dodaje nasem Context objektu neke dodatne opcije za koriscenje, npr u startup klasi.

        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {
        }

        public DbSet<DocumentStatus> DocumentStatus {  get; set; }

        public DbSet<GuaranteeType> GuaranteeTypes { get; set; }

        public DbSet<Document> Document { get; set; }

        public DbSet<ContractLease> contractLeases { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Document Status
            builder.Entity<DocumentStatus>().HasData(
                new
                {
                    DocStatusID = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                    Status = "Usvojen"
                });

            builder.Entity<DocumentStatus>().HasData(
                new
                {
                    DocStatusID = Guid.Parse("458adb42-62a5-4117-8101-7d933fa88abb"),
                    Status = "Odbijen"
                });

            builder.Entity<DocumentStatus>().HasData(
                new
                {
                    DocStatusID = Guid.Parse("84ff030b-7067-45b7-8bb2-10719534f91e"),
                    Status = "Otvoren"
                });


            //Document
            builder.Entity<Document>().HasData(
               new
               {
                   DocumentId = Guid.Parse("07af89f2-feee-4680-b489-9d0e31699588"),
                   RegistrationNumber = "119833332",
                   DocumentCreationDate= new DateTime(),
                   DocumentDate = new DateTime(),
                   DocumentTemplate ="Kreiranje predloga plana",
                   DocStatusID= Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),

               });
            builder.Entity<Document>().HasData(
               new
               {
                   DocumentId = Guid.Parse("3a3e6366-3a20-4d3b-ae15-be85ba277683"),
                   RegistrationNumber = "122267432",
                   DocumentCreationDate = new DateTime(),
                   DocumentDate = new DateTime(),
                   DocumentTemplate = "Obrazovanje komisije za davanje misljenja",
                   DocStatusID = Guid.Parse("458adb42-62a5-4117-8101-7d933fa88abb"),

               });
            builder.Entity<Document>().HasData(
               new
               {
                   DocumentId = Guid.Parse("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"),
                   RegistrationNumber = "119834232",
                   DocumentCreationDate = new DateTime(),
                   DocumentDate = new DateTime(),
                   DocumentTemplate = "Saglasnost ministra",
                   DocStatusID = Guid.Parse("84ff030b-7067-45b7-8bb2-10719534f91e"),

               });
           

            //Guarantee Type

            builder.Entity<GuaranteeType>().HasData(
               new
               {
                   GuaranteeTypeID = Guid.Parse("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"),
                   Type = "Jemstvo"
                  
               });
            builder.Entity<GuaranteeType>().HasData(
               new
               {
                   GuaranteeTypeID = Guid.Parse("ce8229bf-0853-4ae9-b0ed-59c9e5607d64"),
                   Type = "Bankarska garancija"

               });
            builder.Entity<GuaranteeType>().HasData(
               new
               {
                   GuaranteeTypeID = Guid.Parse("372d9458-a560-4b56-8119-ada1f7feb723"),
                   Type = "Garancija nekretnine"

               });
            builder.Entity<GuaranteeType>().HasData(
               new
               {
                   GuaranteeTypeID = Guid.Parse("e54364be-1fe6-43b5-9401-8b8bd2165aba"),
                   Type = "Zirantska"

               });
            builder.Entity<GuaranteeType>().HasData(
              new
              {
                  GuaranteeTypeID = Guid.Parse("68bf5d70-f26b-4c53-b014-bab74b7b86a0"),
                  Type = "Uplata gotovine"

              });


        }
    }
}
