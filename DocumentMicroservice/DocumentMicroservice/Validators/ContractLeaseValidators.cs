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
            RuleFor(contractLease => contractLease.serialNumber)
                .NotEmpty()
                .NotNull();

                RuleFor(contractLease => contractLease.placeOfSigning)
                .NotEmpty()
                .NotNull();

        }
    }
}
