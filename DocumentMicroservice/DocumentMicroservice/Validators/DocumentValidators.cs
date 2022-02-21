using DocumentMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Validators
{
    public class DocumentValidators : AbstractValidator<Document>
    {
        public DocumentValidators()
        {
            RuleFor(document => document.registrationNumber)
                .NotEmpty()
                .NotNull()
                .Matches("^[0-9]+(/[0-9]+)*$");

            RuleFor(document => document.documentTemplate)
               .NotEmpty()
               .NotNull();
             

        }
    }
}
