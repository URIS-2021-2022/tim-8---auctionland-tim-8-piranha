using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using ComplaintMicroservice.Entities.Event;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Entities
{
    public class ComplaintContext : DbContext
    {
        public ComplaintContext(DbContextOptions<ComplaintContext> options) : base(options)
        {

        }

        public DbSet<ComplaintTypeModel> ComplaintTypes { get; set; }
        public DbSet<ComplaintStatus> ComplaintStatus { get; set; }
        public DbSet<ComplaintEvent> ComplaintEvent { get; set; }
        public DbSet<ComplaintMicroservice.Entities.Complaint.Complaint> Complaint { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //COMPLAINT TYPE
            modelBuilder.Entity<ComplaintTypeModel>()
                .HasData(new 
                {
                    ComplaintTypeId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ComplaintType = "Zalba na tok javnog nadmetanja"
                });

            modelBuilder.Entity<ComplaintTypeModel>()
                .HasData(new
                {
                    ComplaintTypeId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ComplaintType = "Zalba na Odluku o davanju u zakup"
                });

            //COMPLAINT STATUS
            modelBuilder.Entity<ComplaintStatus>()
                .HasData(new
                {
                    ComplaintStatusId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Status = "Usvojena"
                });

            modelBuilder.Entity<ComplaintStatus>()
                .HasData(new
                {
                    ComplaintStatusId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Status = "Odbijena"
                });

            //COMPLAINT EVENT
            modelBuilder.Entity<ComplaintEvent>()
                .HasData(new
                {
                    ComplaintEventId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    Event = "JN ide u drugi krug sa novim uslovima"
                });

            modelBuilder.Entity<ComplaintEvent>()
                .HasData(new
                {
                    ComplaintEventId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    Event = "JN ide u drugi krug sa starim uslovima"
                });

            //COMPLAINT
            modelBuilder.Entity<ComplaintMicroservice.Entities.Complaint.Complaint>()
                .HasData(new
                {
                    ComplaintId = Guid.Parse("eb6bac2d-aea4-485a-8cb6-991bf8b1e1d4"),
                    SubmissionDate = DateTime.Parse("06-06-2021"),
                    Explanation = "Complaint explanation",
                    Reason = "Complaint reason",
                    SolutionNumber = "SN001",
                    DecisionNumber = "DN001",
                    ComplaintTypeId= Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ComplaintStatusId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ComplaintEventId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    PublicBiddingId = Guid.Parse("d7d314b0-2f22-4af5-8909-238b23383249"),
                    BuyerId = Guid.Parse("861f142c-4707-416d-ad14-7debbd2031ed")
                });

            modelBuilder.Entity<ComplaintMicroservice.Entities.Complaint.Complaint>()
                .HasData(new
                {
                    ComplaintId = Guid.Parse("b16abef5-5d4e-43a5-9bf3-1fe0618da6f7"),
                    SubmissionDate = DateTime.Parse("05-05-2021"),
                    Explanation = "Complaint explanation 2",
                    Reason = "Complaint reason 2",
                    SolutionNumber = "SN002",
                    DecisionNumber = "DN002",
                    ComplaintTypeId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ComplaintStatusId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ComplaintEventId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    PublicBiddingId = Guid.Parse("d7d314b0-2f22-4af5-8909-238b23383249"),
                    BuyerId = Guid.Parse("861f142c-4707-416d-ad14-7debbd2031ed")
                });
        }
    }
}
