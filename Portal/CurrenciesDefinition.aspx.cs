﻿using Portal.Application.ModelDTOs;
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
            btnUpdate.ServerClick += BtnUpdate_ServerClick;
            btnDelete.ServerClick += BtnDelete_ServerClick;
            btnGet.ServerClick += BtnGet_ServerClick;
        }

        private void BtnGet_ServerClick(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(GetCurrencyAsync));
        }

        private void BtnDelete_ServerClick(object sender, EventArgs e)
        {
            _currencyService.DeleteCurrencyByNumericCode(Convert.ToInt16(txtInputCurrencyNumericCode.Text), 1);
        }

        private void BtnUpdate_ServerClick(object sender, EventArgs e)
        {
            int currentUserId = 1;
            CurrencyDTO currencyDTO = new CurrencyDTO(
                Convert.ToInt16(txtInputCurrencyNumericCode.Text),
                txtEntity.Text,
                txtCurrencyType.Text,
                txtAlphabeticCode.Text,
                txtExchangeRate.Text.ToDecimal(),
                currentUserId);
            _currencyService.UpdateCurrency(currencyDTO);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
        private async Task GetCurrencyAsync()
        {
            var currency = await _currencyService.GetCurrencyAsync(Convert.ToInt16(txtInputCurrencyNumericCode.Text));
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
                txtInputCurrencyNumericCode.Text.Toshort(),
                txtEntity.Text,
                txtCurrencyType.Text,
                txtAlphabeticCode.Text,
                txtExchangeRate.Text.ToDecimal(),
                currentUserId);
            _currencyService.AddCurrency(currencyDTO);
        }
    }
}