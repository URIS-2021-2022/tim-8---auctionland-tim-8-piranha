using AutoMapper;
using ComplaintMicroservice.Entities.Complaint;
using ComplaintMicroservice.Models.Complaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Profiles.Complaint
{
    public class ComplaintConfirmationProfile : Profile
    {
        public ComplaintConfirmationProfile()
        {
            CreateMap<ComplaintConfirmation, ComplaintConfirmationDto>();
            CreateMap<ComplaintMicroservice.Entities.Complaint.Complaint, ComplaintConfirmation>();
        }
    }
}
