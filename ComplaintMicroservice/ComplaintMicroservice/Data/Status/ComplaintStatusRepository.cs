using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.ComplaintStatusEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data.Status
{
    public class ComplaintStatusRepository : IComplaintStatusRepository
    {

        private readonly ComplaintContext context;
        private readonly IMapper mapper;

        public ComplaintStatusRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ComplaintStatusConfirmation CreateComplaintStatus(ComplaintStatus complaintStatus)
        {
            var createdEntity = context.Add(complaintStatus);
            context.SaveChanges();
            return mapper.Map<ComplaintStatusConfirmation>(createdEntity.Entity);

        }

        public void DeleteComplaintStatus(Guid complaintStatusId)
        {
            var status = GetComplaintStatusById(complaintStatusId);
            context.Remove(status);
            context.SaveChanges();
        }

        public List<ComplaintStatus> GetComplaintStatuses(string Status = null)
        {
            return context.ComplaintStatus.Where(e => Status == null || e.Status == Status).ToList();

        }

        public ComplaintStatus GetComplaintStatusById(Guid complaintStatusId)
        {
            return context.ComplaintStatus.FirstOrDefault(e => e.ComplaintStatusId == complaintStatusId);
        }

        public ComplaintStatusConfirmation UpdateComplaintStatus(ComplaintStatus complaintStatus)
        {
            //NE GLEDAJ OVAJ KOD   
            ComplaintStatus cs = GetComplaintStatusById(complaintStatus.ComplaintStatusId);

            cs.ComplaintStatusId = complaintStatus.ComplaintStatusId;
            cs.Status = complaintStatus.Status;

            return new ComplaintStatusConfirmation
            {
                ComplaintStatusId = cs.ComplaintStatusId,
                Status = cs.Status
            };
        }
    }
}
