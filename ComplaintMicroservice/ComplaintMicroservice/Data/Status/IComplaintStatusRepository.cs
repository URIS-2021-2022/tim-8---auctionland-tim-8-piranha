using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data.Status
{
    public interface IComplaintStatusRepository
    {
        Task<List<ComplaintStatus>> GetComplaintStatuses(string Status = null);

        Task<ComplaintStatus> GetComplaintStatusById(Guid complaintStatusId);

        Task<ComplaintStatusConfirmation> CreateComplaintStatus(ComplaintStatus complaintStatus);

        Task UpdateComplaintStatus(ComplaintStatus complaintStatus);

        Task DeleteComplaintStatus(Guid complaintStatusId);

        Task<bool> SaveChanges();
    }
}
