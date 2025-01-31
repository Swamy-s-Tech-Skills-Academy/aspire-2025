using Microsoft.Data.SqlClient;
using Products.Application.Abstractions.Data;
using System.Data.Common;

namespace Products.Infrastructure.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentException("Connection string cannot be null or empty.", nameof(_connectionString)); ;
    }

    public async Task<DbConnection> CreateConnection()
    {
        try
        {
            SqlConnection connection = new(_connectionString);
            await connection.OpenAsync();

            return connection;
        }
        catch (Exception ex)
        {
            // TODO: Log exception
            throw new InvalidOperationException("Could not open SQL connection.", ex);
        }
    }
}
