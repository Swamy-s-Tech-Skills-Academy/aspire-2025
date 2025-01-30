using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;

namespace Products.Infrastructure.Repositories;

internal sealed class CategoriesRepository : ICategoriesRepository
{
    public Task<List<Category>> GetAllCategoriesAsync()
    {
        throw new NotImplementedException();
    }
}
