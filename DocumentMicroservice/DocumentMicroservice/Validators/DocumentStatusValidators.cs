using DocumentMicroservice.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentMicroservice.Validators
{
    public class DocumentStatusValidators : AbstractValidator<DocumentStatus>
    {
        public DocumentStatusValidators()
        {
            RuleFor(documentStatus => documentStatus.Status)
                .NotEmpty()
                .NotNull();
                
        }
        
    }
}
