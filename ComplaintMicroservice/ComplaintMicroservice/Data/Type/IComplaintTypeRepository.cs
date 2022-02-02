using ComplaintMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data
{
    public interface IComplaintTypeRepository
    {
        List<ComplaintTypeModel> GetComplaintTypes(string complaintType=null);

        ComplaintTypeModel GetComplaintTypeById(Guid complaintTypeId);

        ComplaintTypeConfirmation CreateComplaintType(ComplaintTypeModel complaintType);

        ComplaintTypeConfirmation UpdateComplaintType(ComplaintTypeModel complaintType);

        void DeleteComplaintType(Guid complaintTypeId);

    }
}
