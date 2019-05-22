using FluentValidation;
using Portal.Application.Commands;
using Portal.Application.ModelDTOs;

namespace Portal.Application.Validations
{
    public class DefinationCurrencyCommandValidator : AbstractValidator<DefinationCurrencyCommand>
    {
        public DefinationCurrencyCommandValidator()
        {
            RuleFor(c => c.CurrencyDTO.CurrencyNumericCode)
                .NotEmpty().WithMessage("CurrencyNumericCode must be filled.")
                .GreaterThan(1).WithMessage("CurrencyNumericCode must be greater than 1.");
            RuleFor(c => c.CurrencyDTO.AlphabeticCode).NotEmpty().WithMessage("AlphabeticCode must be filled.");
            RuleFor(c => c.CurrencyDTO.Entity).NotEmpty().WithMessage("Entity must be filled.");
            RuleFor(c => c.CurrencyDTO.CurrencyType).NotEmpty().WithMessage("CurrencyType must be filled.");
            RuleFor(c => c.CurrencyDTO.ExchangeRate).
                NotEmpty().WithMessage("ExchangeRate must be filled.")
                .GreaterThan(1).WithMessage("ExchangeRate must be greater than 1.");            
        }
    }
}
