namespace AuthMicroservice.Utils
{
    using AuthMicroservice.Domain;
    using Microsoft.EntityFrameworkCore;
    using System;

    /// <summary>
    /// Database context.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Default constructor for database context.
        /// </summary>
        public DatabaseContext() : base()
        {
        }

        /// <summary>
        /// Constructor for the database context.
        /// </summary>
        /// <param name="options">Options to be applied to database context.</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// What to do on configuring.
        /// </summary>
        /// <param name="options">Options to apply.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(""); // Fallback connection string
            }
        }

        /// <summary>
        /// What to do on model creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string superUserTypeUid = Guid.NewGuid().ToString();

            #region Creating user types

            modelBuilder.Entity<UserType>().HasData(
                new
                {
                    Uid = superUserTypeUid,
                    Name = "Superuser"
                });
            modelBuilder.Entity<UserType>().HasData(
                new
                {
                    Uid = Guid.NewGuid().ToString(),
                    Name = "Operater"
                });
            modelBuilder.Entity<UserType>().HasData(
                new
                {
                    Uid = Guid.NewGuid().ToString(),
                    Name = "Tehnicki sekretar"
                });
            modelBuilder.Entity<UserType>().HasData(
                new
                {
                    Uid = Guid.NewGuid().ToString(),
                    Name = "Prva komisija"
                });
            modelBuilder.Entity<UserType>().HasData(
                new
                {
                    Uid = Guid.NewGuid().ToString(),
                    Name = "Operater nadmetanja"
                });
            modelBuilder.Entity<UserType>().HasData(
                new
                {
                    Uid = Guid.NewGuid().ToString(),
                    Name = "Licitant"
                });
            modelBuilder.Entity<UserType>().HasData(
                new
                {
                    Uid = Guid.NewGuid().ToString(),
                    Name = "Menadzer"
                });
            modelBuilder.Entity<UserType>().HasData(
                new
                {
                    Uid = Guid.NewGuid().ToString(),
                    Name = "Administrator"
                });

            #endregion

            #region Creating Clients

            modelBuilder.Entity<Client>().HasData(
                new
                {
                    Uid = Guid.NewGuid().ToString(),
                    FirstName = "Stefan",
                    LastName = "Radojevic",
                    Username = "Stefi99R",
                    Password = "Qwerty1!",
                    UserTypeUid = superUserTypeUid
                });

            #endregion
        }
    }
}
