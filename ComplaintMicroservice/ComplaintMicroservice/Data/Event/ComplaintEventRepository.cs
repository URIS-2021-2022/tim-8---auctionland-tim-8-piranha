using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.Event;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data.Event
{
    public class ComplaintEventRepository : IComplaintEventRepository
    {
        private readonly ComplaintContext context;
        private readonly IMapper mapper;

        public ComplaintEventRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<ComplaintEventConfirmation> CreateComplaintEvent(ComplaintEvent complaintEvent)
        {
            var createdEntity = await context.ComplaintEvent.AddAsync(complaintEvent);
            context.SaveChanges();
            return mapper.Map<ComplaintEventConfirmation>(createdEntity.Entity);
        }

        public async Task DeleteComplaintEvent(Guid complaintEventId)
        {
            var ev = await GetComplaintEventById(complaintEventId);
            context.ComplaintEvent.Remove(ev);
            context.SaveChanges();
        }

        public async Task<List<ComplaintEvent>> GetComplaintEvents(string complaintEvent = null)
        {
            return await context.ComplaintEvent.Where(e => complaintEvent == null || e.Event == complaintEvent).ToListAsync();

        }

        public async Task<ComplaintEvent> GetComplaintEventById(Guid complaintEventId)
        {
            return await context.ComplaintEvent.FirstOrDefaultAsync(e => e.ComplaintEventId == complaintEventId);
        }

#pragma warning disable CS1998
        public async Task UpdateComplaintEvent(ComplaintEvent complaintEvent)
        {
            //NE GLEDAJ OVAJ KOD   
            
        }
#pragma warning restore CS1998
    }
}
