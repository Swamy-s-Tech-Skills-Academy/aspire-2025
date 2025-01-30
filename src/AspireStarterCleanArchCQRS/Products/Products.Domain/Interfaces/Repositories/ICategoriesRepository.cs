using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Repositories;

public interface ICategoriesRepository
{
    Task<List<Category>> GetAllCategoriesAsync();
}