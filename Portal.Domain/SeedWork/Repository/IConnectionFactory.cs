using System.Data;

namespace Portal.Domain.SeedWork.Repository
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}