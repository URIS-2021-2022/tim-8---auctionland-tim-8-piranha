using BuyerMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Validators
{
    public class ContactPersonValidator : AbstractValidator<ContactPerson>
    {
        public ContactPersonValidator()
        {
            RuleFor(contactPerson => contactPerson.name)
                .NotEmpty()
                .NotNull();


            RuleFor(contactPerson => contactPerson.surname)
                .NotEmpty()
                .NotNull();
                

            RuleFor(contactPerson => contactPerson.phone)
                .NotEmpty()
                .NotNull()
                .Matches("^[0-9]+(/[0-9]+)*$");


        }
    }
}
