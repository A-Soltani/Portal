using System;
using System.Data;

namespace Portal.Infrastructure
{
    public class AdoNetUnitOfWork
    {
        private IDbTransaction _transaction;
        private readonly Action<AdoNetUnitOfWork> _rolledBack;
        private readonly Action<AdoNetUnitOfWork> _committed;
        public IDbTransaction Transaction => _transaction;

        public AdoNetUnitOfWork(IDbTransaction transaction, Action<AdoNetUnitOfWork> rolledBack, Action<AdoNetUnitOfWork> committed)
        {
            _transaction = transaction;
            _rolledBack = rolledBack;
            _committed = committed;
        }

        public void Dispose()
        {
            if (_transaction == null)
                return;

            _transaction.Rollback();
            _transaction.Dispose();
            _rolledBack(this);
            _transaction = null;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException("May not call save changes twice.");

            _transaction.Commit();
            _committed(this);
            _transaction = null;
        }

    }
}