using AutoMapper;
using MediatR;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using System;

using System.Threading;
using System.Threading.Tasks;

namespace Portal.Application.Commands
{
    // MediatR commands
    // In IRequestHandler, there is the inpput DefinationCurrencyCommand and one outpput that is boolean value.
    public class DefinationCurrencyCommandHandler : IRequestHandler<DefinationCurrencyCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        // Using DI to inject infrastructure persistence Repositories
        //public DefinationCurrencyCommandHandler(IMediator mediator, ICurrencyRepository currencyRepository, IMapper mapper)
        //{
        //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        //    _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        //}

        public DefinationCurrencyCommandHandler(IMediator mediator, ICurrencyRepository currencyRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        }

        // MediatR commands
        // This method handels message. It is responsible for getting inpput message and returning proper output.
        public async Task<bool> Handle(DefinationCurrencyCommand message, CancellationToken cancellationToken)
        {            
            // Validation ...

            Currency currency = Currency.CurrencyDefinition(message.Currency.CurrencyNumericCode,
                message.Currency.Entity,
                message.Currency.CurrencyType,
                message.Currency.AlphabeticCode,
                message.Currency.ExchangeRate,
                message.UserID);

            await _currencyRepository.Add(currency);

            return true;
        }

    }
}
