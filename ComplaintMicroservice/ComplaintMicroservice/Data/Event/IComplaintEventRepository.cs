using ComplaintMicroservice.Entities.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data.Event
{
    public interface IComplaintEventRepository
    {
        List<ComplaintEvent> GetComplaintEvents(string complaintEvent = null);

        ComplaintEvent GetComplaintEventById(Guid complaintEventId);

        ComplaintEventConfirmation CreateComplaintEvent(ComplaintEvent complaintEvent);

        void UpdateComplaintEvent(ComplaintEvent complaintEvent);

        void DeleteComplaintEvent(Guid complaintEventId);

        bool SaveChanges();
    }
}
