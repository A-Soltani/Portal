using FluentValidation;
using Portal.Application.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Validations
{
    public class CurrencyValidator : AbstractValidator<CurrencyDTO>
    {
        public CurrencyValidator()
        {
            RuleFor(c => c.CurrencyNumericCode)
                .NotEmpty().WithMessage("CurrencyNumericCode must be filled.")
                .GreaterThan(1).WithMessage("CurrencyNumericCode must be greater than 1.");
            RuleFor(c => c.AlphabeticCode).NotEmpty().WithMessage("AlphabeticCode must be filled.");
            RuleFor(c => c.Entity).NotEmpty().WithMessage("Entity must be filled.");
            RuleFor(c => c.CurrencyType).NotEmpty().WithMessage("CurrencyType must be filled.");
            RuleFor(c => c.ExchangeRate).
                NotEmpty().WithMessage("ExchangeRate must be filled.")
                .GreaterThan(1).WithMessage("ExchangeRate must be greater than 1.");            
        }
    }
}
