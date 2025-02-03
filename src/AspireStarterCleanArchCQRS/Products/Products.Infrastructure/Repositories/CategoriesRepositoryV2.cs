using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;

namespace Products.Infrastructure.Repositories;

internal sealed class CategoriesRepositoryV2(SqlConnection sqlConnection, ILogger<CategoriesRepository> logger)
    : ICategoriesRepository
{
    private readonly SqlConnection _sqlConnection = sqlConnection ?? throw new ArgumentNullException(nameof(sqlConnection));
    private readonly ILogger<CategoriesRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all categories");

        try
        {
            using var command = new SqlCommand("SELECT Id, Name, Description FROM Categories", _sqlConnection); // Your SQL query
            _logger.LogDebug(command.CommandText);

            using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var categories = new List<Category>();
            while (await reader.ReadAsync(cancellationToken))
            {
                var category = new Category
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2)
                };
                categories.Add(category);
            }

            _logger.LogInformation("Successfully retrieved all categories");

            return categories;
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Error getting all categories. SQL Error Code: {ErrorCode}", ex.ErrorCode); // Log with more detail
            throw;
        }
        catch (InvalidOperationException ex) // Example of handling another exception type
        {
            _logger.LogError(ex, "Invalid operation while getting categories");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while getting categories");
            throw;
        }
    }

}
