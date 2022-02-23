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

           


            RuleFor(buyer => buyer.realizedArea)
            .NotEmpty()
            .NotNull();
            

             RuleFor(buyer => buyer.durationOfBanInYear)
            .NotEmpty()
            .NotNull();

<<<<<<< HEAD
            RuleFor(buyer => buyer.name)
                .NotEmpty()
                .NotNull();
                
=======
            RuleFor(individual => individual.name)
                .NotEmpty()
                .NotNull()
                .Matches("^[a - zA - Z] *$");
>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a

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
