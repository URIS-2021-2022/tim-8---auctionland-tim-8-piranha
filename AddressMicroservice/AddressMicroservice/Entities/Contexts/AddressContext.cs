using AddressMicroservice.Entities;
using Microsoft.EntityFrameworkCore;
using System;



namespace AddressMicroservice.Entities.Contexts
{
    public class AddressContext : DbContext
    {
        // Dodaje nasem Context objektu neke dodatne opcije za koriscenje, npr u startup klasi.

        public AddressContext(DbContextOptions<AddressContext> options) : base(options)
        {
        }

        public DbSet<State> State { get; set; }


        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //State
            builder.Entity<State>().HasData(
                new
                {
                    StateID = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),
                    NameState = "Hrvatska"
                });

            builder.Entity<State>().HasData(
                new
                {
                    StateID = Guid.Parse("458adb42-62a5-4117-8101-7d933fa88abb"),
                    NameState = "Srbija"
                });

            builder.Entity<State>().HasData(
                new
                {
                    StateID = Guid.Parse("84ff030b-7067-45b7-8bb2-10719534f91e"),
                    NameState = "Makedonija"
                });


            //Address
            builder.Entity<Address>().HasData(
               new
               {
                   AddressId = Guid.Parse("07af89f2-feee-4680-b489-9d0e31699588"),
                   Street = "Jadranska avenija",
                   StreetNumber = "23b",
                   Place = "Zagreb",
                   ZipCode = "10000",
                   StateID = Guid.Parse("93a08cc2-1d17-46e6-bd95-4fa70bb11226"),

               });
            builder.Entity<Address>().HasData(
               new
               {
                   AddressId = Guid.Parse("3a3e6366-3a20-4d3b-ae15-be85ba277683"),
                   Street = "Svetog Save",
                   StreetNumber = "5",
                   Place = "Pancevo",
                   ZipCode = "26000",
                   StateID = Guid.Parse("458adb42-62a5-4117-8101-7d933fa88abb"),

               });
            builder.Entity<Address>().HasData(
               new
               {
                   AddressId = Guid.Parse("0ec20a3b-fd39-4c2e-8062-7d1664eb5381"),
                   Street = "Drezdenska",
                   StreetNumber = "10",
                   Place = "Skoplje",
                   ZipCode = "1010",
                   StateID = Guid.Parse("84ff030b-7067-45b7-8bb2-10719534f91e"),

               });
        }
    }
}