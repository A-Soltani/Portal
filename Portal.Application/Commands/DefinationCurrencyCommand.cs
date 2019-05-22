using MediatR;
using Portal.Application.ModelDTOs;


namespace Portal.Application.Commands
{
    public class DefinationCurrencyCommand : IRequest<bool>
    {
        public CurrencyDTO CurrencyDTO { get; set; }
        public int UserID { get; set; }     
    }
}
