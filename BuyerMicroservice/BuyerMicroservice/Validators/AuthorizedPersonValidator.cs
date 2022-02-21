using BuyerMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Validators
{
    public class AuthorizedPersonValidator : AbstractValidator<AuthorizedPerson>
    {
        public AuthorizedPersonValidator()
        {
            RuleFor(authorizedPerson => authorizedPerson.name)
                .NotEmpty()
                .NotNull();


            RuleFor(authorizedPerson => authorizedPerson.surname)
                .NotEmpty()
                .NotNull();



            RuleFor(authorizedPerson => authorizedPerson.personalDocNum)
            .NotEmpty()
            .NotNull()
            .Length(13);



            RuleFor(authorizedPerson => authorizedPerson.address)
            .NotEmpty()
            .NotNull();



            RuleFor(authorizedPerson => authorizedPerson.country)
            .NotEmpty()
            .NotNull();
                

        }
    }
}
