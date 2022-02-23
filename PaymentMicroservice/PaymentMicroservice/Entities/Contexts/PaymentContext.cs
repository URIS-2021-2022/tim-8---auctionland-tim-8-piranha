using PaymentMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using System;



namespace PaymentMicroservice.Entities.Contexts
{
    public class PaymentContext : DbContext
    {
        // Dodaje nasem Context objektu neke dodatne opcije za koriscenje, npr u startup klasi.

        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payment { get; set; }

        public DbSet<Course> Course { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            //Payment
            builder.Entity<Payment>().HasData(
               new
               {
                   PaymentId = Guid.Parse("07af89f2-feee-4680-b489-9d0e31699588"),
                   AccountNumber = "56285695865825",
                   ReferenceNUmber = "256352",
                   Amount = "20000",
                   PurposeOfPayment = "Uplata prve rate",
                   PaymentDate = new DateTime(),
                   CourseID = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),

               });
            builder.Entity<Payment>().HasData(
               new
               {
                   PaymentId = Guid.Parse("3a3e6366-3a20-4d3b-ae15-be85ba277683"),
                   AccountNumber = "52468596522558",
                   ReferenceNUmber = "452256",
                   Amount = "30000",
                   PurposeOfPayment = "Uplata druge rate",
                   PaymentDate = new DateTime(),
                   CourseID = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),

               });

            //Course
            builder.Entity<Course>().HasData(
                new
                {
                    CourseID = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                    Currency = "EUR",
                    Value = "118",
                    CourseDate = new DateTime(),

                });

            builder.Entity<Course>().HasData(
                new
                {
                    CourseID = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                    Currency = "USD",
                    Value = "105",
                    CourseDate = new DateTime(),

                });

        }
    }
}