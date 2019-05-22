using AutoMapper;
using MediatR;
using Portal.Application.ModelDTOs;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Application.Queries
{
    public class GetCurrencyByNumericCodeQueryHandler : IRequestHandler<GetCurrencyByNumericCodeQuery, CurrencyDTO>
    {
        private readonly IMediator _mediator;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        public GetCurrencyByNumericCodeQueryHandler(IMediator mediator, ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CurrencyDTO> Handle(GetCurrencyByNumericCodeQuery message, CancellationToken cancellationToken)
        {
            var currency = await  _currencyRepository.GetCurrencyByNumericCodeAsync(message.NumericCode);
            return _mapper.Map<Currency, CurrencyDTO>(currency);
        }
    }
}
