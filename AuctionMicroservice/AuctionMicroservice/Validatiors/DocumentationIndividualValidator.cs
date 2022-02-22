using AuctionMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Validatiors
{
    public class DocumentationIndividualValidator : AbstractValidator<DocumentationIndividual>
    {
        public DocumentationIndividualValidator()
        {
            RuleFor(d => d.FirstName).NotEmpty().NotEqual(d => d.Surname).WithMessage("First name must be different from surname and not be null!");
            RuleFor(d => d.Surname).NotEmpty().WithMessage("Surname must be defined!");
            RuleFor(d => d.AuctionId).NotEmpty().WithMessage("AuctioId must be defined!");
            RuleFor(d => d.IdentificationNumber).NotEmpty().WithMessage("Identification number must be defined!");
            RuleFor(d => d.IdentificationNumber).Matches("^(0|[1-9][0-9]*)$").WithMessage("Identification number must contain numbers only!");
        }
    }
}
