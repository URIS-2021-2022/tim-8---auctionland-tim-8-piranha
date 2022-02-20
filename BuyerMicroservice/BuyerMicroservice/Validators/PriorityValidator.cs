using BuyerMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Validators
{
    public class PriorityValidator : AbstractValidator<Priority>
    {

        public PriorityValidator()
        {
            RuleFor(priority => priority.priorityType)
                 .NotEmpty()
                 .NotNull()
                 .Matches("^[a - zA - Z] *$");
           
        }
    }
}
