using MediatR;
using Portal.Application.ModelDTOs;


namespace Portal.Application.Commands
{
    public class DefinationCurrencyCommand : IRequest<bool>
    {
        public int CurrencyNumericCode { get; set; }
        public string Entity { get; set; }
        public string CurrencyType { get; set; }
        public string AlphabeticCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public int UserID { get; set; }
        public string InsertDate { get; set; }
        public string UpdateDate { get; set; }
    }
}
