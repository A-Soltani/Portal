using Portal.Application.DTO;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application
{
    public interface ICurrencyService
    {
        Currency AddCurrency(CurrencyDTO currencyDTO);

        void UpdateCurrency(CurrencyDTO currencyDTO);
        
        void DeleteCurrencyByNumericCode(short CurrencyNumericCode, int userId);

        Task<List<Currency>> GetCurrencyAsync(Int16? CurrencyNumericCode);
    }
}
