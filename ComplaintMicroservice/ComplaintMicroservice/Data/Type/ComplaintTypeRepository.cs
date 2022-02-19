using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Data
{
    public class ComplaintTypeRepository : IComplaintTypeRepository
    {
        private readonly ComplaintContext context;
        private readonly IMapper mapper;
        
        public ComplaintTypeRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ComplaintTypeConfirmation CreateComplaintType(ComplaintTypeModel complaintType)
        {
            var createdEntity = context.ComplaintTypes.Add(complaintType);
            context.SaveChanges();
            return mapper.Map<ComplaintTypeConfirmation>(createdEntity.Entity); 
        }

        public void DeleteComplaintType(Guid complaintTypeId)
        {
            var type = GetComplaintTypeById(complaintTypeId);
            context.ComplaintTypes.Remove(type);
            context.SaveChanges();
        }

        public List<ComplaintTypeModel> GetComplaintTypes(string complaintType = null)
        {
            return context.ComplaintTypes.Where(e => complaintType == null || e.ComplaintType==complaintType).ToList();
                        
        }

        public ComplaintTypeModel GetComplaintTypeById(Guid complaintTypeId)
        {
            return context.ComplaintTypes.FirstOrDefault(e => e.ComplaintTypeId == complaintTypeId);
        }

        public void UpdateComplaintType(ComplaintTypeModel complaintType)
        {
            //NE GLEDAJ OVAJ KOD   
            /*ComplaintTypeModel ct = GetComplaintTypeById(complaintType.ComplaintTypeId);

            ct.ComplaintTypeId = complaintType.ComplaintTypeId;
            ct.ComplaintType = complaintType.ComplaintType;

            return new ComplaintTypeConfirmation
            {
                ComplaintTypeId = ct.ComplaintTypeId,
                ComplaintType = ct.ComplaintType
            };*/
        }

        /*
         * public static List<ComplaintTypeModel> ComplaintTypes { get; set; } = new List<ComplaintTypeModel>();

        public ComplaintTypeRepository()
        {
            FillData();
        }

        private void FillData()
        {
            ComplaintTypes.AddRange(new List<ComplaintTypeModel>
            {
                new ComplaintTypeModel
                {
                    ComplaintTypeId=Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    ComplaintType="Zalba na tok javnog nadmetanja"
                },
                new ComplaintTypeModel
                {
                    ComplaintTypeId=Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    ComplaintType="Zalba na Odluku o davanju u zakup"
                }
            });
        }

        public ComplaintTypeConfirmation CreateComplaintType(ComplaintTypeModel complaintType)
        {
            complaintType.ComplaintTypeId = Guid.NewGuid();
            ComplaintTypes.Add(complaintType);
            ComplaintTypeModel ct = GetComplaintTypeById(complaintType.ComplaintTypeId);

            return new ComplaintTypeConfirmation
            {
                ComplaintTypeId = ct.ComplaintTypeId,
                ComplaintType = ct.ComplaintType
            };
        }

        public void DeleteComplaintType(Guid complaintTypeId)
        {
            ComplaintTypes.Remove(ComplaintTypes.FirstOrDefault(e => e.ComplaintTypeId == complaintTypeId));
        }

        public List<ComplaintTypeModel> GetComplaintTypes(string complaintType = null)
        {
            return (from e in ComplaintTypes
                    where string.IsNullOrEmpty(complaintType) || e.ComplaintType == complaintType
                    select e).ToList();
        }

        public ComplaintTypeModel GetComplaintTypeById(Guid complaintTypeId)
        {
            return ComplaintTypes.FirstOrDefault(e => e.ComplaintTypeId == complaintTypeId);
        }

        public ComplaintTypeConfirmation UpdateComplaintType(ComplaintTypeModel complaintType)
        {
            ComplaintTypeModel ct = GetComplaintTypeById(complaintType.ComplaintTypeId);

            ct.ComplaintTypeId = complaintType.ComplaintTypeId;
            ct.ComplaintType = complaintType.ComplaintType;

            return new ComplaintTypeConfirmation
            {
                ComplaintTypeId = ct.ComplaintTypeId,
                ComplaintType = ct.ComplaintType
            };
        }
        */
    }
}
