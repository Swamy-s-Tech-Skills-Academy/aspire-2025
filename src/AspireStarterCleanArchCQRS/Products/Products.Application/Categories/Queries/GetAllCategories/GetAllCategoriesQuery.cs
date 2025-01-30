using MediatR;
using Products.Shared.DTOs;

namespace Products.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<List<CategoryDto>>;