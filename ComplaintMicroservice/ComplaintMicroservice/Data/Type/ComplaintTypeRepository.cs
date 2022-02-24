using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> SaveChanges()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<ComplaintTypeConfirmation> CreateComplaintType(ComplaintTypeModel complaintType)
        {
            var createdEntity = await context.ComplaintTypes.AddAsync(complaintType);
            context.SaveChanges();
            return mapper.Map<ComplaintTypeConfirmation>(createdEntity.Entity); 
        }

        public async Task DeleteComplaintType(Guid complaintTypeId)
        {
            var type = await GetComplaintTypeById(complaintTypeId);
            context.ComplaintTypes.Remove(type);
            context.SaveChanges();
        }

        public async Task<List<ComplaintTypeModel>> GetComplaintTypes(string complaintType = null)
        {
            return await context.ComplaintTypes.Where(e => complaintType == null || e.ComplaintType == complaintType).ToListAsync();
                        
        }

        public async Task<ComplaintTypeModel> GetComplaintTypeById(Guid complaintTypeId)
        {
            return await context.ComplaintTypes.FirstOrDefaultAsync(e => e.ComplaintTypeId == complaintTypeId);
        }

#pragma warning disable CS1998
        public async Task UpdateComplaintType(ComplaintTypeModel complaintType)
        {
            //EF sam odradi
        }
#pragma warning restore CS1998

        
    }
}
