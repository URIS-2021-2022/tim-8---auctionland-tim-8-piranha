using AutoMapper;
using PaymentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using PaymentMicroservice.Entities.Contexts;
using PaymentMicroservice.Data.Interfaces;
using PaymentMicroservice.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace PaymentMicroservice.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly PaymentContext Context;
        private readonly IMapper Mapper;

        public CourseRepository(PaymentContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public CourseConfirmation CreateCourse(Course Course)
        {
            var createdEntity = Context.Add(Course);

            return Mapper.Map<CourseConfirmation>(createdEntity.Entity);
        }

        public void DeleteCourse(Course course)
        {
            Context.Remove(course);
        }

        public List<Course> GetCourse()
        {
            return Context.Course
                .AsNoTracking()
                .ToList();
        }


        public Course GetCourseById(Guid CourseId)
        {
            var course = Context.Course.FirstOrDefault(o => o.CourseId == CourseId);

            if (course == null)
            {
                throw new NotFoundException(nameof(Payment), CourseId);
            }

            return course;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
