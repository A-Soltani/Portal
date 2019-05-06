using Portal.Application.DTO;
using Portal.Application.Services;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using Portal.Infrastructure;
using Portal.Infrastructure.Repositories;
using Portal.Infrastructure.Repositories.DapperRepositories;
using Portal.UtilityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class CurrenciesDefinition : System.Web.UI.Page
    {
        #region Services

        private readonly CurrencyService _currencyService;

        public CurrenciesDefinition()
        {
            //_currencyService = _currencyService ?? new CurrencyService(new AdoNetCurrencyRepository());
            _currencyService = _currencyService ?? new CurrencyService(new DapperCurrencyRepository());
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {            
            btnConfirm.ServerClick += new EventHandler(BtnConfirm_ServerClickNew);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(GetCurrencyAsync));
        }
        private async Task GetCurrencyAsync()
        {            
            var currency = await _currencyService.GetCurrencyAsync(8);
            txtInputCurrencyNumericCode.Text = currency.FirstOrDefault().CurrencyNumericCode.ToString();
            txtEntity.Text = currency.FirstOrDefault().Entity.ToString();
            txtCurrencyType.Text = currency.FirstOrDefault().CurrencyType.ToString();
            txtAlphabeticCode.Text = currency.FirstOrDefault().AlphabeticCode.ToString();
            txtExchangeRate.Text = currency.FirstOrDefault().ExchangeRate.ToString();
        }

        void BtnConfirm_ServerClickNew(object sender, EventArgs e)
        {
            int currentUserId = 1;
            CurrencyDTO currencyDTO = new CurrencyDTO(
                txtInputCurrencyNumericCode.Text.ToInt16(),
                txtEntity.Text,
                txtCurrencyType.Text,
                txtAlphabeticCode.Text,
                txtExchangeRate.Text.ToDecimal(),
                currentUserId);
            _currencyService.AddCurrency(currencyDTO);
        }
    }
}