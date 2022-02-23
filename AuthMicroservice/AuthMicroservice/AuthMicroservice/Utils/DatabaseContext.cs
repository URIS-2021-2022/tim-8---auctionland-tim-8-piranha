namespace AuthMicroservice.Utils
{
    using AuthMicroservice.Domain;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("A FALLBACK CONNECTION STRING");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string userTypeUid = Guid.NewGuid().ToString();

            modelBuilder.Entity<UserType>().HasData(
                new
                {
                    Uid = userTypeUid,
                    Name = "Operater"
                });

            modelBuilder.Entity<Client>().HasData(
                new
                {
                    Uid = Guid.NewGuid().ToString(),
                    FirstName = "Stefan",
                    LastName = "Radojevic",
                    Username = "Stefi99R",
                    Password = "knjg",
                    UserTypeUid = userTypeUid
                });
        }
    }
}
