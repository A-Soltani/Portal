using Portal.Application.ModelDTOs;
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
        
        void DeleteCurrencyByNumericCode(short currencyNumericCode, int userId);

        Task<List<Currency>> GetCurrencyAsync(short? currencyNumericCode);

        Task<Currency> GetCurrencyByNumericCodeAsync(short? currencyNumericCode);
    }
}
