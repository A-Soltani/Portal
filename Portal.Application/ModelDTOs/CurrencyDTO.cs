
namespace Portal.Application.ModelDTOs
{
    public class CurrencyDTO
    {
        public int CurrencyNumericCode { get; set; }
        public string Country { get; set; }
        public string CurrencyType { get; set; }
        public string AlphabeticCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public int UserID { get; set; }
        public string InsertDate { get; set; }
        public string UpdateDate { get; set; }
    }
}
