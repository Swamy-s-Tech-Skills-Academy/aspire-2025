using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Repositories;

public interface ICategoriesRepositoryV2
{
    Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);
}