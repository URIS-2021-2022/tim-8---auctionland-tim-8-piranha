using BuyerMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Validators
{
    public class IndividualValidator : AbstractValidator<Individual>
    {
        public IndividualValidator()
        {

            RuleFor(individual => individual.surname)
                .NotEmpty()
                .NotNull()
                .Matches("^[a - zA - Z] *$");

            RuleFor(individual => individual.JMBG)
                .NotEmpty()
                .NotNull()
                .Length(13)
                .Matches("^[0-9]+(/[0-9]+)*$");

            

        }
    }
}
