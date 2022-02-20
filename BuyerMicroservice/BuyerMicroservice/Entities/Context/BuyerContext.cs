﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Entities.Context
{
    public class BuyerContext : DbContext
    {
        public BuyerContext(DbContextOptions<BuyerContext> options) : base(options)
        {

        }

        

        public DbSet<Priority> priority { get; set; }

        public DbSet<ContactPerson> contactPerson { get; set; }

        public DbSet<Individual> individual { get; set; }
        
        public DbSet<LegalEntity> legalEntity { get; set; }

        public DbSet<Buyer> buyer { get; set; }

        public DbSet<AuthorizedPerson> authorizedPerson { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Authorized person 
            builder.Entity<AuthorizedPerson>().HasData(
               new
               {
                   authorizedPersonID = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                   name="Dimitrije",
                   surname="Corlija",
                   personalDocNum="8767834637274",
                   address="Mira popare 11",
                   country="Srbija",
                   
               });

            builder.Entity<AuthorizedPerson>().HasData(
               new
               {
                   authorizedPersonID = Guid.Parse("07af89f2-feee-4680-b489-9d0e31699588"),
                   name = "Marko",
                   surname = "Markovic",
                   personalDocNum = "8227834666274",
                   address = "Bulevar Oslobodjenja 55",
                   country = "Zrenjanin",
                   
               });



            //Board number
            /*builder.Entity<BoardNumber>().HasData(
               new
               {
                  
                   boardNum = 1
                  
               });
            builder.Entity<BoardNumber>().HasData(
              new
              {
               
                  boardNum = "2"
              });*/


            
           
            //Contact person 
            builder.Entity<ContactPerson>().HasData(
              new
              {
                  contactPersonID = Guid.Parse("e54364be-1fe6-43b5-9401-8b8bd2165aba"),
                  name = "Petar",
                  surname = "Petrovic",
                  phone="0629349583"

              });
            builder.Entity<ContactPerson>().HasData(
              new
              {
                  contactPersonID = Guid.Parse("68bf5d70-f26b-4c53-b014-bab74b7b86a0"),
                  name = "Miljan",
                  surname = "Peric",
                  phone = "06559349583"

              });
            //Individual
            builder.Entity<Individual>().HasData(
              new
              {
                  buyerID = Guid.Parse("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"),
                  IsIndividual = true,
                  authorizedPersonID= Guid.Parse("07af89f2-feee-4680-b489-9d0e31699588"),
                  durationOfBanInYear = 1,
                  hasBan = true,
                  startDateOfBan = new DateTime(),
                  endDateOfBan = new DateTime(),
                  realizedArea = 3,
                  priorityID = Guid.Parse("784c7edd-c937-45e6-a493-f0b8dedab85f"),
                  name = "Dino",
                  surname = "Ristic",
                  JMBG = "1102999765578",
                  addresse="Prvomajska 5",
                  phone1="062987999",
                  phone2="-0654442223",
                  email="dinoR@gmail.com",
                  accountNumber="4224234876",
                  

              });

           

            //Legal entity
            builder.Entity<LegalEntity>().HasData(
             new
             {
                  buyerID = Guid.Parse("861f142c-4707-416d-ad14-7debbd2031ed"),
                 IsIndividual=false,
                 authorizedPersonID=Guid.Parse("07af89f2-feee-4680-b489-9d0e31699588"),
                 durationOfBanInYear = 1,
                 hasBan = true,
                 startDateOfBan = new DateTime(),
                 endDateOfBan = new DateTime(),
                 realizedArea = 3,
                 priorityID = Guid.Parse("784c7edd-c937-45e6-a493-f0b8dedab85f"),
                 name = "Rosa",
                 identificationNumber = "12121212333",
                 addresse = "8765439744578",
                 phone1 = "061999999",
                 phone2 = "067662529",
                 fax= "212693-2377",
                 email = "rosa@gmail.com",
                 accountNumber = "0074234876",
                 

             });


            //Priority

            builder.Entity<Priority>().HasData(
            new
            {
                priorityID = Guid.Parse("784c7edd-c937-45e6-a493-f0b8dedab85f"),
                priorityType = "1",
                

            });

            builder.Entity<Priority>().HasData(
            new
            {
                priorityID = Guid.Parse("21200907-0d08-44f3-8506-dc807ca2215b"),
                priorityType = "2",


            });
        }
    }
}