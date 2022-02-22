using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.Complaint;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data.Complaint
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ComplaintContext context;
        private readonly IMapper mapper;

        public ComplaintRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<ComplaintConfirmation> CreateComplaint(ComplaintMicroservice.Entities.Complaint.Complaint complaint)
        {
            var createdEntity = await context.Complaint.AddAsync(complaint);
            context.SaveChanges();
            return mapper.Map<ComplaintConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteComplaint(Guid complaintId)
        {
            var com = await GetComplaintById(complaintId);
            context.Complaint.Remove(com);
            context.SaveChanges();
        }

        public async Task<List<ComplaintMicroservice.Entities.Complaint.Complaint>> GetComplaints(string solutionNumber = null)
        {
            return await context.Complaint.Include(c => c.ComplaintType).Include(c=> c.ComplaintStatus).Include(c=> c.ComplaintEvent).
                Where(c => solutionNumber == null || c.SolutionNumber == solutionNumber).ToListAsync();

        }

        public async Task<ComplaintMicroservice.Entities.Complaint.Complaint> GetComplaintById(Guid complaintId)
        {
            return await context.Complaint.Include(c => c.ComplaintType).Include(c => c.ComplaintStatus).Include(c => c.ComplaintEvent).
                FirstOrDefaultAsync(e => e.ComplaintId == complaintId);
        }

#pragma warning disable CS199
        public async Task UpdateComplaint(ComplaintMicroservice.Entities.Complaint.Complaint complaint)
        {
            await context.SaveChangesAsync();   
            
        }
#pragma warning restore CS1998
    }
}
