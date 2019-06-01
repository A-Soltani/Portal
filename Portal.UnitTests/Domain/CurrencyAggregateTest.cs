using Portal.Domain.AggregatesModel.CurrencyAggregate;
using Portal.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Portal.UnitTests.Domain
{
    public class CurrencyAggregateTest
    {

        [Theory]
        [InlineData(-1, "FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", 1, 1)]
        [InlineData(1, "", "FakeCurrencyType", "FakeAlphabeticCode", 1, 1)]
        [InlineData(1, "FakeCountrytName", "", "FakeAlphabeticCode", 1, 1)]
        [InlineData(1, "FakeCountrytName", "FakeCurrencyType", "", 1, 1)]
        [InlineData(1, "FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", -1, 1)]
        public void CurrencyDefinition_Invalid_Arguments(int currencyNumericCode, string country, string currencyType, string alphabeticCode, decimal exchangeRate, int userId)
        {
            //Act - Assert
            Assert.Throws<PortalDomainException>(() => Currency.CurrencyDefinition(currencyNumericCode, country, currencyType, alphabeticCode, exchangeRate, userId));
        }

        [Theory]
        [InlineData("NewFakeCountrytName", "NewFakeCurrencyType", "NewFakeAlphabeticCode", 10, 10)]
        [InlineData("FakeCountrytName", "NewFakeCurrencyType", "NewFakeAlphabeticCode", 10, 10)]
        [InlineData("FakeCountrytName", "FakeCurrencyType", "NewFakeAlphabeticCode", 10, 10)]
        [InlineData("FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", 10, 10)]
        [InlineData("FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", 1, 10)]
        public void UpdateCurrency_Invalid_Arguments(string country, string currencyType, string alphabeticCode, decimal exchangeRate, int userId)
        {
            //Arrange
            int currencyNumericCode = 1;
            var currency = Currency.CurrencyDefinition(currencyNumericCode, "FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", 1, 1);
            var currencyResultOfUpdating = Currency.CurrencyDefinition(currencyNumericCode, country, currencyType, alphabeticCode, exchangeRate, userId);

            //Act
            currency.UpdateCurrency(country, currencyType, alphabeticCode, exchangeRate, userId);

            //Assert
            Assert.Equal(currency.Country, currencyResultOfUpdating.Country);
            Assert.Equal(currency.CurrencyType, currencyResultOfUpdating.CurrencyType);
            Assert.Equal(currency.AlphabeticCode, currencyResultOfUpdating.AlphabeticCode);
            Assert.Equal(currency.ExchangeRate, currencyResultOfUpdating.ExchangeRate);
            Assert.Equal(currency.UserID, currencyResultOfUpdating.UserID);
        }


    }
}
