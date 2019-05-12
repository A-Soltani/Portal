using FluentValidation;
using MediatR;
using Portal.Application.Validations;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Application.Commands
{
    public class DefinationCurrencyCommandHandler : IRequestHandler<DefinationCurrencyCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ICurrencyRepository _currencyRepository;

        // Using DI to inject infrastructure persistence Repositories
        public DefinationCurrencyCommandHandler(IMediator mediator,
            ICurrencyRepository currencyRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        }

        public async Task<bool> Handle(DefinationCurrencyCommand message, CancellationToken cancellationToken)
        {            
            // Validation ...

            Currency currency = Currency.CurrencyDefinition(message.Currency.CurrencyNumericCode,
                message.Currency.Entity,
                message.Currency.CurrencyType,
                message.Currency.AlphabeticCode,
                message.Currency.ExchangeRate,
                message.Currency.UserID);

            await _currencyRepository.Add(currency);

            return true;
        }




    }
}
