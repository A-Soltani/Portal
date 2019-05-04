using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.DTO
{
    public class CurrencyDTO
    {
        public short CurrencyNumericCode { get; private set; }
        public string Entity { get; private set; }
        public string CurrencyType { get; private set; }
        public string AlphabeticCode { get; private set; }
        public decimal ExchangeRate { get; private set; }
        public int UserID { get; private set; }

        public CurrencyDTO(short currencyNumericCode, string entity, string currencyType, string alphabeticCode, decimal exchangeRate, int userID)
        {
            CurrencyNumericCode = currencyNumericCode;
            Entity = entity;
            CurrencyType = currencyType;
            AlphabeticCode = alphabeticCode;
            ExchangeRate = exchangeRate;
            UserID = userID;
        }
    }
}
