using Portal.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.AggregatesModel.CurrencyAggregate
{
    // DDD Patterns comment
    // This is just the RepositoryContracts or Interface defined at the Domain Layer
    // as requisite for the Currency Aggregate
    public interface ICurrencyRepository : IRepository<Currency>
    {
        Currency Add(Currency currency);

        void Update(Currency currency);

        // Why there are different parameter types for CurrencyNumericCode?
        //public BasketResult<bool> Delete(Int32 CurrencyNumericCode, int userId)
        void DeleteByCurrencyNumericCode(short currencyNumericCode, int userId);

        Task<List<Currency>> GetCurrencyAsync(short? currencyNumericCode);

        Task<Currency> GetCurrencyByNumericCodeAsync(short? currencyNumericCode);

    }
}
