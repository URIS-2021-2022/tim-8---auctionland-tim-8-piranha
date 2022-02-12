using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data.Status
{
    public interface IComplaintStatusRepository
    {
        List<ComplaintStatus> GetComplaintStatuses(string Status = null);

        ComplaintStatus GetComplaintStatusById(Guid complaintStatusId);

        ComplaintStatusConfirmation CreateComplaintStatus(ComplaintStatus complaintStatus);

        ComplaintStatusConfirmation UpdateComplaintStatus(ComplaintStatus complaintStatus);

        void DeleteComplaintStatus(Guid complaintStatusId);
    }
}
