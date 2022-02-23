using BuyerMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Validators
{
    public class LegalEntityValidator : AbstractValidator<LegalEntity>
    {
        public LegalEntityValidator()
        {

            

            RuleFor(legalEntity => legalEntity.identificationNumber)
                .NotEmpty()
                .NotNull()
                .Length(11)
                .Matches("^[0-9]+(/[0-9]+)*$");

            

           

            RuleFor(legalEntity => legalEntity.fax)
               .NotEmpty()
               .NotNull();

            
        }
    }
}
