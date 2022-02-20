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
                .NotNull()
                .Matches("^[a - zA - Z] *$");

            RuleFor(authorizedPerson => authorizedPerson.surname)
                .NotEmpty()
                .NotNull()
                .Matches("^[a - zA - Z] *$");

            
                RuleFor(authorizedPerson => authorizedPerson.personalDocNum)
                .NotEmpty()
                .NotNull()
                .Length(13)
                .Matches("^[a - zA - Z0 - 9] *$");
            

                RuleFor(authorizedPerson => authorizedPerson.address)
                .NotEmpty()
                .NotNull()
                .Matches("^[a - zA - Z0 - 9] *$");

            
                RuleFor(authorizedPerson => authorizedPerson.country)
                .NotEmpty()
                .NotNull()
                .Matches("^[a - zA - Z] *$");

        }
    }
}
