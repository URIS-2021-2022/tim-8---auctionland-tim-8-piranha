using AutoMapper;
using ComplaintMicroservice.Models.Complaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplaintMicroservice.Profiles.Complaint
{
    public class ComplaintProfile : Profile
    {
        public ComplaintProfile()
        {
            CreateMap<ComplaintMicroservice.Entities.Complaint.Complaint, ComplaintDto>();
            CreateMap<ComplaintCreationDto, ComplaintMicroservice.Entities.Complaint.Complaint>();
            CreateMap<ComplaintUpdateDto, ComplaintMicroservice.Entities.Complaint.Complaint>();
            CreateMap<ComplaintMicroservice.Entities.Complaint.Complaint, ComplaintMicroservice.Entities.Complaint.Complaint>();
        }
    }
}
