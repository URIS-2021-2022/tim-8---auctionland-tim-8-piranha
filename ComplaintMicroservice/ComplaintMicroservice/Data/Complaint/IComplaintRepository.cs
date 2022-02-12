using ComplaintMicroservice.Entities.Complaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data
{
    public interface IComplaintRepository
    {
        List<ComplaintMicroservice.Entities.Complaint.Complaint> GetComplaints(string solutionNumber = null);

        ComplaintMicroservice.Entities.Complaint.Complaint GetComplaintById(Guid complaintId);

        ComplaintConfirmation CreateComplaint(ComplaintMicroservice.Entities.Complaint.Complaint complaint);

        ComplaintConfirmation UpdateComplaint(ComplaintMicroservice.Entities.Complaint.Complaint complaint);

        void DeleteComplaint(Guid complaintId);
    }
}
