using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.Event;
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

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ComplaintEventConfirmation CreateComplaintEvent(ComplaintEvent complaintEvent)
        {
            var createdEntity = context.ComplaintEvent.Add(complaintEvent);
            context.SaveChanges();
            return mapper.Map<ComplaintEventConfirmation>(createdEntity.Entity);
        }

        public void DeleteComplaintEvent(Guid complaintEventId)
        {
            var ev = GetComplaintEventById(complaintEventId);
            context.ComplaintEvent.Remove(ev);
            context.SaveChanges();
        }

        public List<ComplaintEvent> GetComplaintEvents(string complaintEvent = null)
        {
            return context.ComplaintEvent.Where(e => complaintEvent == null || e.Event == complaintEvent).ToList();

        }

        public ComplaintEvent GetComplaintEventById(Guid complaintEventId)
        {
            return context.ComplaintEvent.FirstOrDefault(e => e.ComplaintEventId == complaintEventId);
        }

        public ComplaintEventConfirmation UpdateComplaintEvent(ComplaintEvent complaintEvent)
        {
            //NE GLEDAJ OVAJ KOD   
            ComplaintEvent ct = GetComplaintEventById(complaintEvent.ComplaintEventId);

            ct.ComplaintEventId = complaintEvent.ComplaintEventId;
            ct.Event = complaintEvent.Event;

            return new ComplaintEventConfirmation
            {
                ComplaintEventId = ct.ComplaintEventId,
                Event = ct.Event
            };
        }
    }
}
