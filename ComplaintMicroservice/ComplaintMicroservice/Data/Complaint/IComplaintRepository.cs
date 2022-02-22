using ComplaintMicroservice.Entities.Complaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data
{
    public interface IComplaintRepository
    {
        Task<List<ComplaintMicroservice.Entities.Complaint.Complaint>> GetComplaints(string solutionNumber = null);

        Task<ComplaintMicroservice.Entities.Complaint.Complaint> GetComplaintById(Guid complaintId);

        Task<ComplaintConfirmation> CreateComplaint(ComplaintMicroservice.Entities.Complaint.Complaint complaint);

        Task UpdateComplaint(ComplaintMicroservice.Entities.Complaint.Complaint complaint);

        Task DeleteComplaint(Guid complaintId);

        Task<bool> SaveChanges();
    }
}
