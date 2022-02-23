using AuctionMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionMicroservice.Validatiors
{
    public class AuctionValidator : AbstractValidator<Auction>
    {
        public AuctionValidator()
        {
            
            RuleFor(a => a.AuctionNum).NotEmpty().WithMessage("Auction number can't be empty!");
            RuleFor(a => a.Year).NotEmpty().Equal(a => a.Date.Year).WithMessage("Year of the auction must be the same as year from the date attribute!");
            RuleFor(a => a.Restriction).NotEmpty().WithMessage("Restriction number must be defined!");
            RuleFor(a => a.PriceStep).NotEmpty().WithMessage("Price step must be defined!");
            RuleFor(a => a.ApplicationDeadline).NotEmpty().LessThan(a => a.Date).WithMessage("Application deadline must be before auction starts!");

        }
    }
}
