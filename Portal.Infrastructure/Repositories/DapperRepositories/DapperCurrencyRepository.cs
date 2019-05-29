using AutoMapper;
using Dapper;
using Portal.Application.ModelDTOs;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using Portal.Domain.SeedWork;
using Portal.Infrastructure.Repositories.DapperRepositories.SeedWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.DapperRepositories
{
    public class DapperCurrencyRepository : ICurrencyRepository
    {
        //private readonly DapperContext _context;

        private readonly IMapper _mapper;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public IConnectionFactory DefaultConnection { get; private set; }

        public DapperCurrencyRepository(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            DefaultConnection = new DbConnectionFactory("MyConString");
        }

        public async Task<Currency> Add(Currency currency)
        {
            using (var _context = new DapperContext(DefaultConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IN_CurrencyNumericCode", currency.CurrencyNumericCode);
                parameters.Add("@IN_Entity", currency.Country);
                parameters.Add("@IN_CurrencyType", currency.CurrencyType);
                parameters.Add("@IN_AlphabeticCode", currency.AlphabeticCode);
                parameters.Add("@IN_ExchangeRate", currency.ExchangeRate);
                parameters.Add("@IN_UserID", currency.UserID);
                await _context.Connection.QueryAsync<Currency>("MMCCurrencies_Insert", parameters, commandType: CommandType.StoredProcedure);

                return currency;
            }
        }

        public void DeleteByCurrencyNumericCode(int currencyNumericCode, int userId)
        {
            using (var _context = new DapperContext(DefaultConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IN_CurrencyNumericCode", currencyNumericCode);
                parameters.Add("@IN_UserID", userId);
                _context.Connection.Query<Currency>("MMCCurrencies_Delete", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }       

        public async Task<List<Currency>> GetCurrencyAsync(int? currencyNumericCode)
        {
            using (var _context = new DapperContext(DefaultConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IN_CurrencyNumericCode", currencyNumericCode);

                // Dapper is used to execute MMCCurrencies_Get sp and CurrencyDTO is only option to do it.
                // You aren't allowed to apply Currency to output object because it's properties have private setter
                var currencyDTOList = await _context.Connection.QueryAsync<CurrencyDTO>("MMCCurrencies_Get", parameters, commandType: CommandType.StoredProcedure);
                
                return currencyDTOList.Select(c => _mapper.Map<CurrencyDTO, Currency>(c)).ToList();
            }
        }

        public void Update(Currency currency)
        {
            using (var _context = new DapperContext(DefaultConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IN_CurrencyNumericCode", currency.CurrencyNumericCode);
                parameters.Add("@IN_Entity", currency.Country);
                parameters.Add("@IN_CurrencyType", currency.CurrencyType);
                parameters.Add("@IN_AlphabeticCode", currency.AlphabeticCode);
                parameters.Add("@IN_ExchangeRate", currency.ExchangeRate);
                parameters.Add("@IN_UserID", currency.UserID);
                _context.Connection.Query<Currency>("MMCCurrencies_Update", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public async Task<Currency> GetCurrencyByNumericCodeAsync(int? currencyNumericCode)
        {
            using (var _context = new DapperContext(DefaultConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IN_CurrencyNumericCode", currencyNumericCode);

                // Dapper is used to execute MMCCurrencies_Get sp and CurrencyDTO is only option to do it.
                // You aren't allowed to apply Currency to output object because it's properties have private setter
                CurrencyDTO currencyDTO = await _context.Connection.QueryFirstOrDefaultAsync<CurrencyDTO>("MMCCurrencies_Get", parameters, commandType: CommandType.StoredProcedure);
               
                return _mapper.Map<CurrencyDTO, Currency>(currencyDTO);
            }
        }
    }
}
