using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.ModelDTOs
{
    public class CurrencyDTO
    {
        public int CurrencyNumericCode { get; set; }
        public string Entity { get; set; }
        public string CurrencyType { get; set; }
        public string AlphabeticCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public int UserID { get; set; }

        public CurrencyDTO(int currencyNumericCode, string entity, string currencyType, string alphabeticCode, decimal exchangeRate, int userID)
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
