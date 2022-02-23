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




                RuleFor(contractLease => contractLease.placeOfSigning)
                .NotEmpty()
                .NotNull();
=======



            RuleFor(contractLease => contractLease.placeOfSigning)
            .NotEmpty()
            .NotNull();
>>>>>>> Stashed changes
                


        }
    }
}
