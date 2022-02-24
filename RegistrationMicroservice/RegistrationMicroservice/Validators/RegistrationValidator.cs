using FluentValidation;
using RegistrationMicroservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistrationMicroservice.Validators
{
    public class RegistrationValidator : AbstractValidator<Registration>
    {
        public RegistrationValidator()
        {
            RuleFor(d => d.Date).NotEmpty().WithMessage("Date of registration cannot be null!");
            RuleFor(d => d.Location).NotEmpty().WithMessage("Location of registration must be defined!");
            
        }
    }
}
