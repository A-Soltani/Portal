using Portal.Domain.Exceptions;
using Portal.Domain.SeedWork;
using System;

namespace Portal.Domain.AggregatesModel.CurrencyAggregate
{
    public class Currency : Entity, IAggregateRoot
    {
        // DDD Patterns comment
        // Note that it is recommended to implement immutable Commands
        // In this case, its immutability is achieved by having all the setters as private
        // plus only being able to update the data just once, when creating the object through its constructor.
        public int CurrencyNumericCode { get; private set; }
        public string Country { get; private set; }
        public string CurrencyType { get; private set; }
        public string AlphabeticCode { get; private set; }
        public decimal ExchangeRate { get; private set; }
        public int UserID { get; private set; } // It should be move this to repository because this property doesn't play any business role in Currency Country.

        // There isn't any parameter for dates?
        public string InsertDate { get; private set; }
        public string UpdateDate { get; private set; }

        private Currency(int currencyNumericCode, string country, string currencyType, string alphabeticCode, decimal exchangeRate, int userId)
        {
            // What is the validation of currency??

            if (currencyNumericCode < 1)
                throw new PortalDomainException("currencyNumericCode must be greater than 1.");

            this.SetMutableFields(country, currencyType, alphabeticCode, exchangeRate, userId);          
          
        }

        // ToDo Avoid types with poor cohesion
        private void SetMutableFields(string country, string currencyType, string alphabeticCode, decimal exchangeRate, int userId)
        {
            SetCountry(country);
            SetCurrencyType(currencyType);
            SetAlphabeticCode(alphabeticCode);
            SetExchangeRate(exchangeRate);
            this.UserID = userId;
        }

        private void SetExchangeRate(decimal exchangeRate)
        {
            if (exchangeRate < 1)
                throw new PortalDomainException("exchangeRate must be greater than 0.");
            this.ExchangeRate = exchangeRate;
        }

        private void SetAlphabeticCode(string alphabeticCode)
        {
            if (string.IsNullOrWhiteSpace(alphabeticCode))
                throw new PortalDomainException("alphabeticCode must be filled.");
            this.AlphabeticCode = alphabeticCode;
        }

        private void SetCurrencyType(string currencyType)
        {
            if (string.IsNullOrWhiteSpace(currencyType))
                throw new PortalDomainException("currencyType must be filled.");
            this.CurrencyType = currencyType;
        }

        private void SetCountry(string country)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new PortalDomainException("currency must be filled.");
            this.Country = country;
        }

        // DDD Patterns comment
        // This Order AggregateRoot's method "CurrencyDefinition()" should be the only way to add Items to the Order,
        // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
        // in order to maintain consistency between the whole Aggregate. 
        public static Currency CurrencyDefinition(int currencyNumericCode, string country, string currencyType, string alphabeticCode, decimal exchangeRate, int userId)
        {
            return new Currency(currencyNumericCode, country, currencyType, alphabeticCode, exchangeRate, userId);
        }

        public void UpdateCurrency(string country, string currencyType, string alphabeticCode, decimal exchangeRate, int userId)
        {
            // What is the validation of currency?

            this.SetMutableFields(country, currencyType, alphabeticCode, exchangeRate, userId);
           
        }

    }
}
