﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DocumentMicroservice.Entities.Contexts
{
    public class DocumentContext : DbContext
    {
        

        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {
        }

        public DbSet<DocumentStatus> DocumentStatus {  get; set; }

        public DbSet<GuaranteeType> GuaranteeTypes { get; set; }

        public DbSet<Document> Document { get; set; }

        public DbSet<ContractLease> contractLease { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Document Status
            modelBuilder.Entity<DocumentStatus>().HasData(
                new
                {
                    docStatusID = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                    status = "Usvojen"
                });

            modelBuilder.Entity<DocumentStatus>().HasData(
                new
                {
                    docStatusID = Guid.Parse("458adb42-62a5-4117-8101-7d933fa88abb"),
                    status = "Odbijen"
                });

            modelBuilder.Entity<DocumentStatus>().HasData(
                new
                {
                    docStatusID = Guid.Parse("84ff030b-7067-45b7-8bb2-10719534f91e"),
                    status = "Otvoren"
                });


            //Document
            modelBuilder.Entity<Document>().HasData(
               new
               {
                   documentId = Guid.Parse("07af89f2-feee-4680-b489-9d0e31699588"),
                   registrationNumber = "119833332",
                   documentCreationDate= new DateTime(),
                   documentDate = new DateTime(),
                   documentTemplate ="Kreiranje predloga plana",
                   docStatusID= Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),

               });
            modelBuilder.Entity<Document>().HasData(
               new
               {
                   documentId = Guid.Parse("3a3e6366-3a20-4d3b-ae15-be85ba277683"),
                   registrationNumber = "122267432",
                   documentCreationDate = new DateTime(),
                   documentDate = new DateTime(),
                   documentTemplate = "Obrazovanje komisije za davanje misljenja",
                   docStatusID = Guid.Parse("458adb42-62a5-4117-8101-7d933fa88abb"),

               });
            modelBuilder.Entity<Document>().HasData(
               new
               {
                   documentId = Guid.Parse("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"),
                   registrationNumber = "119834232",
                   documentCreationDate = new DateTime(),
                   documentDate = new DateTime(),
                   documentTemplate = "Saglasnost ministra",
                   docStatusID = Guid.Parse("84ff030b-7067-45b7-8bb2-10719534f91e"),

               });


            //Guarantee Type

            modelBuilder.Entity<GuaranteeType>().HasData(
               new
               {
                   guaranteeTypeID = Guid.Parse("f5f92ac7-0682-48a6-bd34-f2f5d89be9a0"),
                   type = "Jemstvo"
                  
               });
            modelBuilder.Entity<GuaranteeType>().HasData(
               new
               {
                   guaranteeTypeID = Guid.Parse("ce8229bf-0853-4ae9-b0ed-59c9e5607d64"),
                   type = "Bankarska garancija"

               });
            modelBuilder.Entity<GuaranteeType>().HasData(
               new
               {
                   guaranteeTypeID = Guid.Parse("372d9458-a560-4b56-8119-ada1f7feb723"),
                   type = "Garancija nekretnine"

               });
            modelBuilder.Entity<GuaranteeType>().HasData(
               new
               {
                   guaranteeTypeID = Guid.Parse("e54364be-1fe6-43b5-9401-8b8bd2165aba"),
                   type = "Zirantska"

               });
            modelBuilder.Entity<GuaranteeType>().HasData(
              new
              {
                  guaranteeTypeID = Guid.Parse("68bf5d70-f26b-4c53-b014-bab74b7b86a0"),
                  type = "Uplata gotovine"

              });

            modelBuilder.Entity<ContractLease>().HasData(
              new
              {
                  contractLeaseID = Guid.Parse("68bf5d70-f26b-4c53-b014-bab74b7b86a0"),
                  serialNumber = "12345",
                  submissionDate = new DateTime(),
                  deadlineLandRestitution = new DateTime(),
                  placeOfSigning = "Zrenjanin",
                  dateOfSigning = new DateTime(),
                  guaranteeTypeID = Guid.Parse("68bf5d70-f26b-4c53-b014-bab74b7b86a0"),
                  documentId = Guid.Parse("3a3e6366-3a20-4d3b-ae15-be85ba277683")
              });

            modelBuilder.Entity<ContractLease>().HasData(
              new
              {
                  contractLeaseID = Guid.Parse("86c9ac76-a632-4ffc-b2a2-26ea8600dc86"),
                  serialNumber = "2342323",
                  submissionDate = new DateTime(),
                  deadlineLandRestitution = new DateTime(),
                  placeOfSigning = "Zrenjanin",
                  dateOfSigning = new DateTime(),
                  guaranteeTypeID = Guid.Parse("68bf5d70-f26b-4c53-b014-bab74b7b86a0"),
                  documentId = Guid.Parse("3a3e6366-3a20-4d3b-ae15-be85ba277683")

              });


        }
    }
}
