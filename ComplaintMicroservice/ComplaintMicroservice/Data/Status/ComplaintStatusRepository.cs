using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data.Status
{
    public class ComplaintStatusRepository : IComplaintStatusRepository
    {

        private readonly ComplaintContext context;
        private readonly IMapper mapper;

        public ComplaintStatusRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<ComplaintStatusConfirmation> CreateComplaintStatus(ComplaintStatus complaintStatus)
        {
            var createdEntity = await context.AddAsync(complaintStatus);
            context.SaveChanges();
            return mapper.Map<ComplaintStatusConfirmation>(createdEntity.Entity);

        }

        public async Task DeleteComplaintStatus(Guid complaintStatusId)
        {
            var status = await GetComplaintStatusById(complaintStatusId);
            context.Remove(status);
            context.SaveChanges();
        }

        public async Task<List<ComplaintStatus>> GetComplaintStatuses(string Status = null)
        {
            return await context.ComplaintStatus.Where(e => Status == null || e.Status == Status).ToListAsync();

        }

        public async Task<ComplaintStatus> GetComplaintStatusById(Guid complaintStatusId)
        {
            return await context.ComplaintStatus.FirstOrDefaultAsync(e => e.ComplaintStatusId == complaintStatusId);
        }

#pragma warning disable CS1998
        public async Task UpdateComplaintStatus(ComplaintStatus complaintStatus)
        {
            //NE GLEDAJ OVAJ KOD   
            
        }
#pragma warning restore CS1998
    }
}
