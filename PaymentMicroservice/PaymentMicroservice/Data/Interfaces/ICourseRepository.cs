using PaymentMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Data.Interfaces
{
    public interface ICourseRepository
    {
        List<Course> GetCourse();

        Course GetCourseById(Guid courseId);

        CourseConfirmation CreateCourse(Course course);

        void DeleteCourse(Course course);

        void SaveChanges();
    }
}