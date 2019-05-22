using MediatR;
using Portal.Application.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Queries
{
    public class GetCurrencyByNumericCodeQuery : IRequest<CurrencyDTO>
    {
        public int? NumericCode { get; set; }
    }
}
