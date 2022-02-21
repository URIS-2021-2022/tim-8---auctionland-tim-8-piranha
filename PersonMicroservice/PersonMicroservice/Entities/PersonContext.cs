using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroservice.Entities
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {

        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Board>()
                .HasMany(b => b.Members)
                .WithMany(p => p.Boards);

            modelBuilder.Entity<Board>()
                .HasOne(b => b.President);


            //Persons
            modelBuilder.Entity<Person>().HasData(
                new
                {
                    PersonId = Guid.Parse("2d8607c5-f3cf-4ef5-9323-a9318eee6232"),
                    Name = "Davor",
                    Surname = "Jelic",
                    Function = "President"
                });

            modelBuilder.Entity<Person>().HasData(
                new
                {
                    PersonId = Guid.Parse("2411cc63-1a91-4bb2-9432-c2f0515cef63"),
                    Name = "Milan",
                    Surname = "Novcic",
                    Function = "Judge"
                });

            modelBuilder.Entity<Person>().HasData(
                new
                {
                    PersonId = Guid.Parse("81f63012-16d7-4f1a-a330-55dc295a6dcd"),
                    Name = "Mihajlo",
                    Surname = "Strajin",
                    Function = "Member"
                });

            //Type
            modelBuilder.Entity<Board>().HasData(
                new
                {
                    BoardId = Guid.Parse("8010f254-e872-49d9-9c2c-1d5783719019"),
                    PresidentId = Guid.Parse("2d8607c5-f3cf-4ef5-9323-a9318eee6232")
                });

            modelBuilder.Entity<Board>().HasData(
                new
                {
                    BoardId = Guid.Parse("e53171cc-91f1-4716-8ea8-39b31a97dd84"),
                    PresidentId = Guid.Parse("2d8607c5-f3cf-4ef5-9323-a9318eee6232")
                });

        }
    }
}
