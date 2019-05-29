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
        [Fact]
        public void CurrencyDefinition_Invalid_Arguments()
        {
            //Act - Assert
            Assert.Throws<PortalDomainException>(() => Currency.CurrencyDefinition(-1, "FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", 1, 1));
            Assert.Throws<PortalDomainException>(() => Currency.CurrencyDefinition(1, "", "FakeCurrencyType", "FakeAlphabeticCode", 1, 1));
            Assert.Throws<PortalDomainException>(() => Currency.CurrencyDefinition(1, "FakeCountrytName", "", "FakeAlphabeticCode", 1, 1));
            Assert.Throws<PortalDomainException>(() => Currency.CurrencyDefinition(1, "FakeCountrytName", "FakeCurrencyType", "", 1, 1));
            Assert.Throws<PortalDomainException>(() => Currency.CurrencyDefinition(1, "FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", -1, 1));
        }

        [Fact]
        public void UpdateCurrency_Invalid_Arguments()
        {
            // Arrange
            var currency = Currency.CurrencyDefinition(1, "FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", 1, 1);

            //Act - Assert
            Assert.Throws<PortalDomainException>(() => currency.UpdateCurrency("", "FakeCurrencyType", "FakeAlphabeticCode", 1, 1));
            Assert.Throws<PortalDomainException>(() => currency.UpdateCurrency("FakeCountrytName", "", "FakeAlphabeticCode", 1, 1));
            Assert.Throws<PortalDomainException>(() => currency.UpdateCurrency("FakeCountrytName", "FakeCurrencyType", "", 1, 1));
            Assert.Throws<PortalDomainException>(() => currency.UpdateCurrency("FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", -1, 1));
        }
    }
}
