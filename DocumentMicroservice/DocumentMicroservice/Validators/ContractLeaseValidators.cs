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
<<<<<<< HEAD
            RuleFor(contractLease => contractLease.serialNumber)
                .NotEmpty()
                .NotNull();
=======
<<<<<<< Updated upstream
          

=======
            /*
            RuleFor(contractLease => contractLease.maturities)
                .NotEmpty()
                .NotNull();
            // .Matches("^[0-9]+(/[0-9]+)*$");
            */
>>>>>>> Stashed changes

            RuleFor(contractLease => contractLease.serialNumber)
                .NotEmpty()
                .NotNull();
<<<<<<< Updated upstream



>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a

                RuleFor(contractLease => contractLease.placeOfSigning)
                .NotEmpty()
                .NotNull();
<<<<<<< HEAD
=======
=======



            RuleFor(contractLease => contractLease.placeOfSigning)
            .NotEmpty()
            .NotNull();
>>>>>>> Stashed changes
                

>>>>>>> fc78dec60f56cc4dd7d5724adaa6d44b10ccb90a

        }
    }
}
