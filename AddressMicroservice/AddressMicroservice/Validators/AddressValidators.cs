using AddressMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Validators
{
    public class AddressValidators : AbstractValidator<Address>
    {
        public AddressValidators()
        {
            RuleFor(address => address.Place)
                .NotEmpty()
                .NotNull();

            RuleFor(address => address.Street)
               .NotEmpty()
               .NotNull();

        }
    }
}