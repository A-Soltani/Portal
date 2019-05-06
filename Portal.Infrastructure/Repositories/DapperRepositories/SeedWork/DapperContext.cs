using Portal.Domain.SeedWork;
using Portal.Domain.SeedWork.Repository;
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
        private readonly LinkedList<AdoNetUnitOfWork> _uows = new LinkedList<AdoNetUnitOfWork>();

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

        public IDbCommand CreateCommand()
        {
            var cmd = _connection.CreateCommand();

            _rwLock.EnterReadLock();
            if (_uows.Count > 0)
                cmd.Transaction = _uows.First.Value.Transaction;
            _rwLock.ExitReadLock();

            return cmd;
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
