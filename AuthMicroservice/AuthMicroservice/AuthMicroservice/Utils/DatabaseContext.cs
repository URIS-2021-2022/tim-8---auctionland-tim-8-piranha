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
