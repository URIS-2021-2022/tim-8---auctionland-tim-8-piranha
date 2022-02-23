using AuctionMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Validatiors
{
    public class DocumentationLegalEntityValidator : AbstractValidator<DocumentationLegalEntity>
    {
        public DocumentationLegalEntityValidator()
        {
            RuleFor(d => d.Name).NotEmpty().WithMessage("Name of the legal entity must be defined");
            RuleFor(d => d.IdentificationNumber).NotEmpty().WithMessage("Identification number must be defined!");
            RuleFor(d => d.Address).NotEmpty().WithMessage("Address must be defined!");
            RuleFor(d => d.AuctionId).NotEmpty().WithMessage("AuctionId must be defined!");

        }

    }
}
