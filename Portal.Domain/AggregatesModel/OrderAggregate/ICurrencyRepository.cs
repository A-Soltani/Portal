using Portal.Domain.Seedwork;
using System.Threading.Tasks;

namespace Portal.Domain.AggregatesModel.OrderAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Currency Aggregate

    public interface ICurrencyRepository : IRepository<Currency>
    {
        Currency Add(Currency currency);
        
        void Update(Currency currency);

        Task<Currency> GetAsync(int currencyId);
    }
}
