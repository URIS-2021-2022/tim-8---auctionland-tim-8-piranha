using AutoMapper;
using PaymentMicroservice.Entities;
using PaymentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentMicroservice.Models.Course;

namespace PaymentMicroservice.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<CourseCreationDto, Course>();
            CreateMap<Course, CourseConfirmation>();
            CreateMap<CourseConfirmation, CourseConfirmationDto>();
            CreateMap<CourseUpdateDto, Course>();
            CreateMap<Course, Course>();
        }
    }
}
