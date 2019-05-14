using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Portal.Infrastructure
{
    public class DbConnectionFactory : IConnectionFactory
    {
        private readonly string _name;
        private readonly DbProviderFactory _provider;
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionName)) throw new Exception("connectionName must be filled.");

            var conStr = ConfigurationManager.ConnectionStrings[connectionName];

            if (conStr == null)
                throw new ConfigurationErrorsException(string.Format("Failed to find connection string named '{0}' in app/web.config.", connectionName));

            _name = conStr.ProviderName;
            _provider = DbProviderFactories.GetFactory(_name);
            _connectionString = conStr.ConnectionString;
        }

        public IDbConnection Create()
        {
            var connection = _provider.CreateConnection();
            if (connection == null)
                throw new ConfigurationErrorsException(string.Format("Failed to create a connection using the connection string named '{0}' in app/web.config.", _name));

            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
    }
}
