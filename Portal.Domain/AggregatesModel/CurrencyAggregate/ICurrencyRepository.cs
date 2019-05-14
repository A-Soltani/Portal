using Portal.Domain.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portal.Domain.AggregatesModel.CurrencyAggregate
{
    // DDD Patterns comment
    // This is just the RepositoryContracts or Interface defined at the Domain Layer
    // as requisite for the Currency Aggregate
    public interface ICurrencyRepository : IRepository<Currency>
    {
        Task<Currency> Add(Currency currency);

        void Update(Currency currency);

        // Why there are different parameter types for CurrencyNumericCode?
        //public BasketResult<bool> Delete(Int32 CurrencyNumericCode, int userId)
        void DeleteByCurrencyNumericCode(int currencyNumericCode, int userId);

        Task<List<Currency>> GetCurrencyAsync(int? currencyNumericCode);

        Task<Currency> GetCurrencyByNumericCodeAsync(int? currencyNumericCode);

    }
}
