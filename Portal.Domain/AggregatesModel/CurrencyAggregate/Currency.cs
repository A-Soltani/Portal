using Portal.Domain.Seedwork;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Domain.AggregatesModel.CurrencyAggregate
{
    public class Currency
        : Entity, IAggregateRoot
    {
        public int NumericCode { get; private set; }
        public string Type { get; private set; }
        public string ExchangeRate { get; private set; }
        public string InsertDate { get; private set; }
        public string UpdateDate { get; private set; }

        private Currency(int numericCode, string type, string exchangeRate, string insertDate, string updateDate)
        {
            if (numericCode < 0 || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(exchangeRate)
                || string.IsNullOrWhiteSpace(insertDate))
                throw new Exception("There is an error");

            NumericCode = numericCode;
            Type = type;
            ExchangeRate = exchangeRate;
            InsertDate = insertDate;
            UpdateDate = updateDate;
        }

        public static Currency DefineCurrency(int numericCode, string type, string exchangeRate, string insertDate, string updateDate)
        {
            return new Currency(numericCode, type, exchangeRate, insertDate, "");
        }

        public void UpdateCurrency(string type, string exchangeRate, string updateDate)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(exchangeRate))
                throw new Exception("There is an error");

            if (Convert.ToDateTime(updateDate) < Convert.ToDateTime(this.InsertDate))
                throw new Exception("UpdateDate must be greater than InsertDate.");

            this.Type = type;
            this.ExchangeRate = exchangeRate;
            this.UpdateDate = updateDate;
        }

        public void DeleteCurrency(int numericCode)
        {
            // ???
        }

    }
}

