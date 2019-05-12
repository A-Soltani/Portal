using AutoMapper;
using Dapper;
using Portal.Application.ModelDTOs;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using Portal.Domain.SeedWork;
using Portal.Infrastructure.Extensions;
using Portal.Infrastructure.Repositories.DapperRepositories.SeedWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.DapperRepositories
{
    public class DapperCurrencyRepository : ICurrencyRepository
    {
        //private readonly DapperContext _context;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public IConnectionFactory DefaultConnection { get; private set; }

        public DapperCurrencyRepository()
        {
            DefaultConnection = new DbConnectionFactory("MyConString");
        }

        public async Task<Currency> Add(Currency currency)
        {
            using (var _context = new DapperContext(DefaultConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IN_CurrencyNumericCode", currency.CurrencyNumericCode);
                parameters.Add("@IN_Entity", currency.Entity);
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

        //public Task<List<Currency>> GetCurrencyAsync(int? currencyNumericCode)
        //{
        //    using (var _context = new DapperContext(DefaultConnection))
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@IN_CurrencyNumericCode", CurrencyNumericCode);

        //        // Dapper is used to execute MMCCurrencies_Get sp and CurrencyDTO is only option to do it.
        //        // You aren't allowed to apply Currency to output object because it's properties have private setter
        //        List<CurrencyDTO> currencyDTOList = _context.Connection.Query<CurrencyDTO>("MMCCurrencies_Get", parameters, commandType: CommandType.StoredProcedure).ToList();

        //        // Following block is used for getting Currency map by AutoMapper
        //        // First, there is a config for the mapper due to keep encapsulation in domain
        //        var config = new MapperConfiguration(cfg =>
        //        {
        //            cfg.CreateMap<CurrencyDTO, Currency>()
        //            .ConstructUsing(cd => Currency.CurrencyDefinition(cd.CurrencyNumericCode, cd.Entity, cd.CurrencyType, cd.AlphabeticCode, cd.ExchangeRate, cd.UserID));
        //        });

        //        // Secondly there is a map between 
        //        IMapper mapper = config.CreateMapper();                
        //        return Task.Run<List<Currency>>(() => currencyDTOList.Select(c => mapper.Map<CurrencyDTO, Currency>(c)).ToList());
        //    }
        //}

        public async Task<List<Currency>> GetCurrencyAsync(int? currencyNumericCode)
        {
            using (var _context = new DapperContext(DefaultConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IN_CurrencyNumericCode", currencyNumericCode);

                // Dapper is used to execute MMCCurrencies_Get sp and CurrencyDTO is only option to do it.
                // You aren't allowed to apply Currency to output object because it's properties have private setter
                var currencyDTOList = await _context.Connection.QueryAsync<CurrencyDTO>("MMCCurrencies_Get", parameters, commandType: CommandType.StoredProcedure);

                // Following block is used for getting Currency map by AutoMapper
                // First, there is a config for the mapper due to keep encapsulation in domain
                var config = new MapperConfiguration(cfg =>
                 {
                     cfg.CreateMap<CurrencyDTO, Currency>()
                      .ConstructUsing(cd => Currency.CurrencyDefinition(cd.CurrencyNumericCode, cd.Entity, cd.CurrencyType, cd.AlphabeticCode, cd.ExchangeRate, cd.UserID));
                 });

                // Secondly there is a map between 
                IMapper mapper = config.CreateMapper();
                return currencyDTOList.Select(c => mapper.Map<CurrencyDTO, Currency>(c)).ToList();
            }
        }

        public void Update(Currency currency)
        {
            using (var _context = new DapperContext(DefaultConnection))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IN_CurrencyNumericCode", currency.CurrencyNumericCode);
                parameters.Add("@IN_Entity", currency.Entity);
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

                // Following block is used for getting Currency map by AutoMapper
                // First, there is a config for the mapper due to keep encapsulation in domain
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CurrencyDTO, Currency>()
                    .ConstructUsing(cd => Currency.CurrencyDefinition(cd.CurrencyNumericCode, cd.Entity, cd.CurrencyType, cd.AlphabeticCode, cd.ExchangeRate, cd.UserID));
                });

                // Secondly there is a map between 
                IMapper mapper = config.CreateMapper();
                return mapper.Map<CurrencyDTO, Currency>(currencyDTO);
            }
        }
    }
}
