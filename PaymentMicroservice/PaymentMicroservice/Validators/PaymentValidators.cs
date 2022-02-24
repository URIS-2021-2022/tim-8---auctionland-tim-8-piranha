using PaymentMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Validators
{
    public class PaymentValidators : AbstractValidator<Payment>
    {
        public PaymentValidators()
        {
            RuleFor(payment => payment.AccountNumber)
                .NotEmpty()
                .NotNull();


            RuleFor(payment => payment.ReferenceNumber)
               .NotEmpty()
               .NotNull();
              

        }
    }
}
