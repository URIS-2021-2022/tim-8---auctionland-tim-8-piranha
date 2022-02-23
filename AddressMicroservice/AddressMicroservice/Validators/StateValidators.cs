using AddressMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressMicroservice.Validators
{
    public class StateValidators : AbstractValidator<State>
    {
        public StateValidators()
        {
            RuleFor(state => state.NameState)
                .NotEmpty()
                .NotNull();
        }

    }
}