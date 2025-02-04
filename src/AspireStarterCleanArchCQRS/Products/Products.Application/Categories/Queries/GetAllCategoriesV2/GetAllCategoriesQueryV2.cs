using MediatR;
using Products.Shared.DTOs;

namespace Products.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQueryV2 : IRequest<List<CategoryDto>>;