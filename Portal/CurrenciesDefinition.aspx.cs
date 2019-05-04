using Portal.Application.DTO;
using Portal.Application.Services;
using Portal.UtilityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal
{
    public partial class CurrenciesDefinition : System.Web.UI.Page
    {
        #region Services

        private readonly CurrencyService _currencyService;

        public CurrenciesDefinition(CurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            btnConfirm.ServerClick += new EventHandler(BtnConfirm_ServerClickNew);
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