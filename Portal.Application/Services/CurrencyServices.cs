using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Application.DTO;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using Portal.Domain.SeedWork.Repository;

namespace Portal.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public Currency AddCurrency(CurrencyDTO currencyDTO)
        {
            Currency currency = Currency.CurrencyDefinition(currencyDTO.CurrencyNumericCode, currencyDTO.Entity, currencyDTO.CurrencyType, currencyDTO.AlphabeticCode, currencyDTO.ExchangeRate, currencyDTO.UserID);
            _currencyRepository.Add(currency);
            return currency;
        }

        public void DeleteCurrencyByNumericCode(short CurrencyNumericCode, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Currency>> GetCurrencyAsync(short? CurrencyNumericCode)
        {
            return _currencyRepository.GetCurrencyAsync(CurrencyNumericCode);
        }

        public async Task<Currency> GetCurrencyByNumericCodeAsync(short? currencyNumericCode)
        {
            return await _currencyRepository.GetCurrencyByNumericCodeAsync(currencyNumericCode);
        }

        public async void UpdateCurrency(CurrencyDTO currencyDTO)
        {
            // It must be checked that corresponding currency is in database. If not then an exception must be thrown.
            Currency currency = await this.GetCurrencyByNumericCodeAsync(currencyDTO.CurrencyNumericCode);
            if (currency == null)
                throw new Exception("There isn't any currency with " + currencyDTO.CurrencyNumericCode + " CurrencyNumericCode.");

            // Update currency in domain
            currency.UpdateCurrency(currencyDTO.Entity, currencyDTO.CurrencyType, currencyDTO.AlphabeticCode, currencyDTO.ExchangeRate, currencyDTO.UserID);

            // Update currency in database
            _currencyRepository.Update(currency);
        }
    }
}
