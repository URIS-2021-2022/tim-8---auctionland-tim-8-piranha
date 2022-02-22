using BuyerMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyerMicroservice.Validators
{
    public class BoardNumberValidator : AbstractValidator<BoardNumber>
    {

        public BoardNumberValidator()
        {
            RuleFor(boardNumber => boardNumber.number)
                 .NotEmpty()
                 .NotNull();


        }
    }
}
