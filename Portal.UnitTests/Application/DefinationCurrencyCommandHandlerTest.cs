using AutoMapper;
using MediatR;
using Moq;
using Portal.Application.Commands;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Portal.UnitTests.Application
{
    public class DefinationCurrencyCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ICurrencyRepository> _currencyRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public DefinationCurrencyCommandHandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _currencyRepositoryMock = new Mock<ICurrencyRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task Handle_return_true_if_currency_is_persisted()
        {
            //Arrange
            var fakeOrderCmd = FakeDefinationCurrencyCommand(new Dictionary<string, object>
            {
                ["currencyNumericCode"] = 1,
                ["country"] = "FakeCountrytName",
                ["alphabeticCode"] = "FakeAlphabeticCode",
                ["currencyType"] = "FakeCurrencyType",
                ["exchangeRate"] = 1.1,
                ["insertDate"] = "FakeInsertDate",
                ["updateDate"] = "FakeUpdateDate",
                ["userID"] = 1,
            });

            _currencyRepositoryMock.Setup(currencyRepo => currencyRepo.GetCurrencyByNumericCodeAsync(It.IsAny<int>()))
                   .Returns(Task.FromResult<Currency>(FakeCurrency()));

            _currencyRepositoryMock.Setup(currencyRepo => currencyRepo.UnitOfWork.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(1));

            //Act
            var handler = new DefinationCurrencyCommandHandler(_mediatorMock.Object, _currencyRepositoryMock.Object, _mapperMock.Object);
            var cltToken = new System.Threading.CancellationToken();
            var result = await handler.Handle(fakeOrderCmd, cltToken);

            //Assert
            Assert.True(result);
        }

        private Currency FakeCurrency()
        {
            return Currency.CurrencyDefinition(1, "FakeCountrytName", "FakeCurrencyType", "FakeAlphabeticCode", 1, 1);
        }

        private DefinationCurrencyCommand FakeDefinationCurrencyCommand(Dictionary<string, object> args = null)
        {
            return new DefinationCurrencyCommand()
            {
                AlphabeticCode = (args != null && args.ContainsKey("alphabeticCode")) ? (string)args["alphabeticCode"] : null,
                Country = (args != null && args.ContainsKey("country")) ? (string)args["country"] : null,
                CurrencyNumericCode = (args != null && args.ContainsKey("currencyNumericCode")) ? (int)args["currencyNumericCode"] : 0,
                CurrencyType = (args != null && args.ContainsKey("currencyType")) ? (string)args["currencyType"] : null,
                ExchangeRate = (args != null && args.ContainsKey("exchangeRate")) ? Convert.ToDecimal(args["exchangeRate"]) : -1,
                InsertDate = args != null && args.ContainsKey("insertDate") ? (string)args["insertDate"] : null,
                UpdateDate = args != null && args.ContainsKey("updateDate") ? (string)args["updateDate"] : null,
                UserID = (args != null && args.ContainsKey("userID")) ? (int)args["userID"] : 0,
            };
        }
    }
}
