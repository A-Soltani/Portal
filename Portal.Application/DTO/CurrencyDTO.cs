using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.DTO
{
    public class CurrencyDTO
    {
        public short CurrencyNumericCode { get; set; }
        public string Entity { get; set; }
        public string CurrencyType { get; set; }
        public string AlphabeticCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public int UserID { get; set; }

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
