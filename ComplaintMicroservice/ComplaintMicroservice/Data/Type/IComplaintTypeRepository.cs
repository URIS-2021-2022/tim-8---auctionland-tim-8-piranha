using ComplaintMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data
{
    public interface IComplaintTypeRepository
    {
        Task<List<ComplaintTypeModel>> GetComplaintTypes(string complaintType=null);

        Task<ComplaintTypeModel> GetComplaintTypeById(Guid complaintTypeId);

        Task<ComplaintTypeConfirmation> CreateComplaintType(ComplaintTypeModel complaintType);

        Task UpdateComplaintType(ComplaintTypeModel complaintType);

        Task DeleteComplaintType(Guid complaintTypeId);

        Task<bool> SaveChanges();

    }
}
