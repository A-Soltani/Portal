using MediatR;
using Portal.Application.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Commands
{
    public class DefinationCurrencyCommand : IRequest<bool>
    {
        public CurrencyDTO Currency { get; private set; }
        public int UserID { get; private set; }

        public DefinationCurrencyCommand(CurrencyDTO currency, int userID)
        {
            Currency = currency;
            UserID = userID;
        }

    }
}
