using System.Data.Common;

namespace Products.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    Task<DbConnection> CreateConnection();
}
