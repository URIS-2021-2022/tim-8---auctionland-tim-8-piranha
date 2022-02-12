using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicBidding.Entities
{
    public class PublicBiddingContext : DbContext
    {
        public PublicBiddingContext (DbContextOptions<PublicBiddingContext> options) : base(options)
        {

        }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<PublicBidding> PublicBiddings { get; set; }
        public DbSet<PublicBiddingAuthorizedPerson> PublicBiddingAuthorizedPerson { get; set; }
        public DbSet<PublicBiddingBuyer> PublicBiddingBuyer { get; set; }
        public DbSet<PublicBiddingPlotPart> PublicBiddingPlotPart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Veze izmedju entiteta van mikroservisa
            modelBuilder.Entity<PublicBiddingAuthorizedPerson>()
                .HasOne(p => p.PublicBidding)
                .WithMany()
                .HasForeignKey("PublicBiddingId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<PublicBiddingAuthorizedPerson>()
                .HasKey(pa => new { pa.PublicBiddingId, pa.AuthorizedPersonId });

            modelBuilder.Entity<PublicBiddingBuyer>()
                .HasOne(p => p.PublicBidding)
                .WithMany()
                .HasForeignKey("PublicBiddingId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<PublicBiddingBuyer>()
                .HasKey(pb => new { pb.PublicBiddingId, pb.BuyerId });

            modelBuilder.Entity<PublicBiddingPlotPart>()
                .HasOne(p => p.PublicBidding)
                .WithMany()
                .HasForeignKey("PublicBiddingId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder.Entity<PublicBiddingPlotPart>()
                .HasKey(pp => new { pp.PublicBiddingId, pp.PlotPartId });

            //Status
            modelBuilder.Entity<Status>().HasData(
                new
                {
                    StatusId = Guid.Parse("2233cbba-607a-4182-9f83-7ff8ffe6e5ac"),
                    StatusName = "Prvi krug"
                });

            modelBuilder.Entity<Status>().HasData(
                new
                {
                    StatusId = Guid.Parse("770a32d4-1db9-4844-868e-6bf8171ffc20"),
                    StatusName = "Drugi krug sa novim uslovima"
                });

            modelBuilder.Entity<Status>().HasData(
                new
                {
                    StatusId = Guid.Parse("28273376-994b-461d-8097-d03654c5268d"),
                    StatusName = "Drugi krug sa starim uslovima"
                });

            //Type
            modelBuilder.Entity<Type>().HasData(
                new
                {
                    TypeId = Guid.Parse("8010f254-e872-49d9-9c2c-1d5783719019"),
                    TypeName = "Javna licitacija"
                });

            modelBuilder.Entity<Type>().HasData(
                new
                {
                    TypeId = Guid.Parse("9b926999-151c-458c-8ae8-3d4a7e9f6459"),
                    TypeName = "Otvaranje zatvorenih ponuda"
                });
        }
    }
}
