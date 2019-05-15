
using Portal.Application.ModelDTOs;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portal.Application.Services
{
    public interface ICurrencyService
    {
        void DeleteCurrencyByNumericCode(int currencyNumericCode, int userId);
        Task<List<Currency>> GetCurrencyAsync(int? currencyNumericCode);
        Task<Currency> GetCurrencyByNumericCodeAsync(int? currencyNumericCode);
        void UpdateCurrency(CurrencyDTO currencyDTO);

    }
}