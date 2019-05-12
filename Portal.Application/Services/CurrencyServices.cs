using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Portal.Application.ModelDTOs;
using Portal.Application.Validations;
using Portal.Domain.AggregatesModel.CurrencyAggregate;

namespace Portal.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository ?? throw new ArgumentNullException(nameof(currencyRepository));
        }

        public bool AddCurrency(CurrencyDTO currencyDTO)
        {
            CurrencyValidator currencyValidator = new CurrencyValidator();
            var validationResult = currencyValidator.Validate(currencyDTO);
            if (!validationResult.IsValid)
                throw new ValidationException("Validation exception", validationResult.Errors);

            Currency currency = Currency.CurrencyDefinition(currencyDTO.CurrencyNumericCode, currencyDTO.Entity, currencyDTO.CurrencyType, currencyDTO.AlphabeticCode, currencyDTO.ExchangeRate, currencyDTO.UserID);
            _currencyRepository.Add(currency);
            return true;

        }

        public void DeleteCurrencyByNumericCode(int currencyNumericCode, int userId)
        {
            _currencyRepository.DeleteByCurrencyNumericCode(currencyNumericCode, userId);
        }

        public Task<List<Currency>> GetCurrencyAsync(int? currencyNumericCode)
        {
            return _currencyRepository.GetCurrencyAsync(currencyNumericCode);
        }

        public async Task<Currency> GetCurrencyByNumericCodeAsync(int? currencyNumericCode)
        {
            return await _currencyRepository.GetCurrencyByNumericCodeAsync(currencyNumericCode);
        }

        public async void UpdateCurrency(CurrencyDTO currencyDTO)
        {
            CurrencyValidator currencyValidator = new CurrencyValidator();
            var validationResult = currencyValidator.Validate(currencyDTO);
            if (validationResult.IsValid)
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
            else
                throw new ValidationException("Validation exception", validationResult.Errors);
        }
    }
}
