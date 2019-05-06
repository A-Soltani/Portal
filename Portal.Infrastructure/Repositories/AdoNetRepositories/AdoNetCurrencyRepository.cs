using Portal.Domain.AggregatesModel.CurrencyAggregate;
using Portal.Domain.SeedWork;
using Portal.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories
{
    public class AdoNetCurrencyRepository : ICurrencyRepository
    {
        private readonly AdoNetContext _context;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public AdoNetCurrencyRepository()
        {
            _context = new AdoNetContext(new DbConnectionFactory("MyConString"));
        }

        public Currency Add(Currency currency)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MMCCurrencies_Insert";
                // Be careful CreateParameter is an extension
                command.Parameters.Add(command.CreateParameter("@IN_CurrencyNumericCode", currency.CurrencyNumericCode));
                command.Parameters.Add(command.CreateParameter("@IN_Entity", currency.Entity));
                command.Parameters.Add(command.CreateParameter("@IN_CurrencyType", currency.CurrencyType));
                command.Parameters.Add(command.CreateParameter("@IN_AlphabeticCode", currency.AlphabeticCode));
                command.Parameters.Add(command.CreateParameter("@IN_ExchangeRate", currency.ExchangeRate));
                command.Parameters.Add(command.CreateParameter("@IN_UserID", currency.UserID));

                return this.ToList(command).FirstOrDefault();
                //return ExecuteNonQuery(StoredProcedures.MMCCurrencies_Insert, parameters, ServerType.MainServer);
            }
        }

        protected List<Currency> ToList(IDbCommand command)
        {
            using (var record = command.ExecuteReader())
            {
                List<Currency> items = new List<Currency>();
                while (record.Read())
                {
                    items.Add(Map<Currency>(record));
                }
                return items;
            }
        }

        private T Map<T>(IDataReader record)
        {
            var objT = Activator.CreateInstance<T>();
            foreach (var property in typeof(T).GetProperties())
            {
                if (record.HasColumn(property.Name) && !record.IsDBNull(record.GetOrdinal(property.Name)))
                    property.SetValue(objT, record[property.Name]);
            }
            return objT;
        }

        public void DeleteByCurrencyNumericCode(short CurrencyNumericCode, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Currency>> GetCurrencyAsync(short? CurrencyNumericCode)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "MMCCurrencies_Get";
                // Be careful CreateParameter is an extension
                command.Parameters.Add(command.CreateParameter("@IN_CurrencyNumericCode", CurrencyNumericCode));
                //List<Currency> test = ;
                return Task.Run<List<Currency>>(() => this.ToList(command));
                //return await this.ToList(command);
            }
        }

        public void Update(Currency currency)
        {
            throw new NotImplementedException();
        }
    }
}
