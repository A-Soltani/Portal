
using AutoMapper;
using MediatR;
using Portal.Application.Commands;
using Portal.Application.Mappings.AutoMapper;
using Portal.Application.ModelDTOs;
using Portal.Application.Queries;
using Portal.Application.Services;
using Portal.Domain.AggregatesModel.CurrencyAggregate;

using Portal.UtilityExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace Portal
{

    public partial class CurrenciesDefinition : System.Web.UI.Page
    {
        int currentUserId = 1;

        public IMediator Mediator { get; set; }

        public IMapper Mapper { get; set; }

        //public ICurrencyService CurrencyService { get; set; }

        #region Services


        public CurrenciesDefinition()
        {
            //_currencyService = _currencyService ?? new CurrencyService(new DapperCurrencyRepository());           
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            btnConfirm.ServerClick += new EventHandler(BtnConfirm_ServerClickNew);
            btnUpdate.ServerClick += BtnUpdate_ServerClick;
            btnDelete.ServerClick += BtnDelete_ServerClick;
            btnGet.ServerClick += BtnGet_ServerClick;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void BtnGet_ServerClick(object sender, EventArgs e)
        {
            try
            {
                RegisterAsyncTask(new PageAsyncTask(GetCurrencyAsync));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BtnDelete_ServerClick(object sender, EventArgs e)
        {
            //CurrencyService.DeleteCurrencyByNumericCode(Convert.ToInt16(txtInputCurrencyNumericCode.Text), 1);
        }

        private void BtnUpdate_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int currentUserId = 1;
                CurrencyDTO currencyDTO = new CurrencyDTO()
                {
                    AlphabeticCode = txtAlphabeticCode.Text,
                    CurrencyNumericCode = Convert.ToInt16(txtInputCurrencyNumericCode.Text),
                    Country = txtCountry.Text,
                    CurrencyType = txtCurrencyType.Text,
                    ExchangeRate = txtExchangeRate.Text.ToDecimal(),
                    UserID = currentUserId
                };
                //CurrencyService.UpdateCurrency(currencyDTO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task GetCurrencyAsync()
        {
            try
            {
                GetCurrencyByNumericCodeQuery getCurrencyByNumericCodeQuery = new GetCurrencyByNumericCodeQuery()
                {
                    NumericCode = Convert.ToInt16(txtInputCurrencyNumericCode.Text)
                };
                var currency = await Mediator.Send(getCurrencyByNumericCodeQuery);

                txtInputCurrencyNumericCode.Text = currency.CurrencyNumericCode.ToString();
                txtCountry.Text = currency.Country.ToString();
                txtCurrencyType.Text = currency.CurrencyType.ToString();
                txtAlphabeticCode.Text = currency.AlphabeticCode.ToString();
                txtExchangeRate.Text = currency.ExchangeRate.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void BtnConfirm_ServerClickNew(object sender, EventArgs e)
        {
            try
            {
                //this.AddCurrency();
                RegisterAsyncTask(new PageAsyncTask(DefinationCurrencyCommand));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async Task DefinationCurrencyCommand()
        {
            try
            {

                DefinationCurrencyCommand definationCurrencyCommand = new DefinationCurrencyCommand()
                {
                    AlphabeticCode = txtAlphabeticCode.Text,
                    CurrencyNumericCode = Convert.ToInt16(txtInputCurrencyNumericCode.Text),
                    Country = txtCountry.Text,
                    CurrencyType = txtCurrencyType.Text,
                    ExchangeRate = txtExchangeRate.Text.ToDecimal(),
                    UserID = currentUserId
                };

                var isSuccess = await Mediator.Send(definationCurrencyCommand);
                if (isSuccess)
                    lblMessage.Text = "عملیات تعریف ارز با موفقیت انجام شد.";
                else
                    lblMessage.Text = "خطایی در تعریف ارز رخ داده است.";
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}