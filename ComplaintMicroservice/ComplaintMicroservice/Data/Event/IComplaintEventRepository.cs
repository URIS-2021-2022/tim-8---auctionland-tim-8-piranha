using ComplaintMicroservice.Entities.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data.Event
{
    public interface IComplaintEventRepository
    {
        Task<List<ComplaintEvent>> GetComplaintEvents(string complaintEvent = null);

        Task<ComplaintEvent> GetComplaintEventById(Guid complaintEventId);

        Task<ComplaintEventConfirmation> CreateComplaintEvent(ComplaintEvent complaintEvent);

        Task UpdateComplaintEvent(ComplaintEvent complaintEvent);

        Task DeleteComplaintEvent(Guid complaintEventId);

        Task<bool> SaveChanges();
    }
}
