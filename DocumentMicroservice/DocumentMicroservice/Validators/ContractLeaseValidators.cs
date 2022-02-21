using DocumentMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Validators
{
    public class ContractLeaseValidators : AbstractValidator<ContractLease>
    {

        public ContractLeaseValidators()
        {
            RuleFor(contractLease => contractLease.maturities)
                .NotEmpty()
                .NotNull();
            // .Matches("^[0-9]+(/[0-9]+)*$");

           
            RuleFor(contractLease => contractLease.serialNumber)
                .NotEmpty()
                .NotNull()
                .Matches("^[a - zA - Z0 - 9] *$");

            
                RuleFor(contractLease => contractLease.placeOfSigning)
                .NotEmpty()
                .NotNull()
                .Matches("^[a - zA - Z0 - 9] *$");


        }
    }
}
