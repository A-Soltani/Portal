using Portal.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Infrastructure.Repositories.DapperRepositories.SeedWork
{
    public class DapperContext : IUnitOfWork
    {
        private IConnectionFactory _connectionFactory;
        private readonly IDbConnection _connection;
        private readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();
        

        public IDbConnection Connection => _connection;

        public DapperContext(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = connectionFactory.Create();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
