using DocumentMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Validators
{
    public class GuaranteeTypeValidators : AbstractValidator<GuaranteeType>
    {
        public GuaranteeTypeValidators()
        {
            RuleFor(guaranteeType => guaranteeType.Type)
                .NotEmpty()
                .NotNull()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
