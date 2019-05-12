using System.Data;

namespace Portal.Infrastructure
{
    public interface IConnectionFactory
    {
        IDbConnection Create();
    }
}