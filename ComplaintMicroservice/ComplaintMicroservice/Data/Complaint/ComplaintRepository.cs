﻿using AutoMapper;
using ComplaintMicroservice.Entities;
using ComplaintMicroservice.Entities.Complaint;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComplaintMicroservice.Data.Complaint
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ComplaintContext context;
        private readonly IMapper mapper;

        public ComplaintRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ComplaintConfirmation CreateComplaint(ComplaintMicroservice.Entities.Complaint.Complaint complaint)
        {
            var createdEntity = context.Complaint.Add(complaint);
            context.SaveChanges();
            return mapper.Map<ComplaintConfirmation>(createdEntity.Entity);
        }

        public void DeleteComplaint(Guid complaintId)
        {
            var com = GetComplaintById(complaintId);
            context.Complaint.Remove(com);
            context.SaveChanges();
        }

        public List<ComplaintMicroservice.Entities.Complaint.Complaint> GetComplaints(string solutionNumber = null)
        {
            return context.Complaint.Include(c => c.ComplaintType).Include(c=> c.ComplaintStatus).Include(c=> c.ComplaintEvent).
                Where(c => solutionNumber == null || c.SolutionNumber == solutionNumber).ToList();

        }

        public ComplaintMicroservice.Entities.Complaint.Complaint GetComplaintById(Guid complaintId)
        {
            return context.Complaint.Include(c => c.ComplaintType).Include(c => c.ComplaintStatus).Include(c => c.ComplaintEvent).
                FirstOrDefault(e => e.ComplaintId == complaintId);
        }

        public void UpdateComplaint(ComplaintMicroservice.Entities.Complaint.Complaint complaint)
        {
            //NE GLEDAJ OVAJ KOD   
            
        }
    }
}
