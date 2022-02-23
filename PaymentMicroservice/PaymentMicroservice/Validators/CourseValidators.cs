using PaymentMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Validators
{
    public class CourseValidators : AbstractValidator<Course>
    {
        public CourseValidators()
        {
            RuleFor(payment => payment.Currency)
                .NotEmpty()
                .NotNull();


            RuleFor(payment => payment.Value)
               .NotEmpty()
               .NotNull();


        }
    }
}
