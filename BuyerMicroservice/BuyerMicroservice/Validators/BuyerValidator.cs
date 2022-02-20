using BuyerMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Validators
{
    public class BuyerValidator : AbstractValidator<Buyer>
    {
        public BuyerValidator()
        {
            RuleFor(buyer => buyer.hasBan)
                .NotEmpty()
                .NotNull();

            RuleFor(buyer => buyer.IsIndividual)
                .NotEmpty()
                .NotNull();


            RuleFor(buyer => buyer.realizedArea)
            .NotEmpty()
            .NotNull();
            

             RuleFor(buyer => buyer.durationOfBanInYear)
            .NotEmpty()
            .NotNull();

            RuleFor(individual => individual.name)
                .NotEmpty()
                .NotNull()
                .Matches("^[a - zA - Z] *$");

            RuleFor(individual => individual.addresse)
               .NotEmpty()
               .NotNull()
               .Matches("^[a - zA - Z] *$");

            RuleFor(individual => individual.phone1)
                .NotEmpty()
                .NotNull()
                .Matches("^[0-9]+(/[0-9]+)*$");

            RuleFor(individual => individual.phone2)
                .NotEmpty()
                .NotNull()
                .Matches("^[0-9]+(/[0-9]+)*$");

            RuleFor(individual => individual.email)
               .NotEmpty()
               .NotNull();

            RuleFor(individual => individual.accountNumber)
               .NotEmpty()
               .NotNull()
               .Length(10)
               .Matches("^[0-9]+(/[0-9]+)*$");
        }
    }
}
