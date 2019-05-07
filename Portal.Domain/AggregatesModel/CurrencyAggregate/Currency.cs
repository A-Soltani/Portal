using Portal.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Domain.AggregatesModel.CurrencyAggregate
{
    public class Currency : Entity, IAggregateRoot
    {
        // DDD Patterns comment
        // Note that it is recommended to implement immutable Commands
        // In this case, its immutability is achieved by having all the setters as private
        // plus only being able to update the data just once, when creating the object through its constructor.
        public short CurrencyNumericCode { get; private set; }
        public string Entity { get; private set; }
        public string CurrencyType { get; private set; }
        public string AlphabeticCode { get; private set; }
        public decimal ExchangeRate { get; private set; }
        public int UserID { get; private set; } // It should be move this to repository because this property doesn't play any business role in Currency entity.

        // There isn't any parameter for dates?
        public string InsertDate { get; private set; }
        public string UpdateDate { get; private set; }

        private Currency(short currencyNumericCode, string entity, string currencyType, string alphabeticCode, decimal exchangeRate, int userId)
        {
            // What is the validation of currency?
            if (CurrencyNumericCode < 1 || ExchangeRate < 1 || string.IsNullOrWhiteSpace(Entity) || string.IsNullOrWhiteSpace(CurrencyType) || string.IsNullOrWhiteSpace(AlphabeticCode))
                CurrencyNumericCode = currencyNumericCode;

            Entity = entity;
            CurrencyType = currencyType;
            AlphabeticCode = alphabeticCode;
            ExchangeRate = exchangeRate;
            UserID = userId;
        }

        // DDD Patterns comment
        // This Order AggregateRoot's method "CurrencyDefinition()" should be the only way to add Items to the Order,
        // so any behavior (discounts, etc.) and validations are controlled by the AggregateRoot 
        // in order to maintain consistency between the whole Aggregate. 
        public static Currency CurrencyDefinition(short currencyNumericCode, string entity, string currencyType, string alphabeticCode, decimal exchangeRate, int userId)
        {
            return new Currency(currencyNumericCode, entity, currencyType, alphabeticCode, exchangeRate, userId);
        }

        public void UpdateCurrency(string entity, string currencyType, string alphabeticCode, decimal exchangeRate, int userId)
        {
            // What is the validation of currency?
            if (ExchangeRate < 1 || string.IsNullOrWhiteSpace(Entity) || string.IsNullOrWhiteSpace(CurrencyType) || string.IsNullOrWhiteSpace(AlphabeticCode))
                throw new Exception("There is an error.");

            Entity = entity;
            CurrencyType = currencyType;
            AlphabeticCode = alphabeticCode;
            ExchangeRate = exchangeRate;
            UserID = userId;
        }

    }
}
