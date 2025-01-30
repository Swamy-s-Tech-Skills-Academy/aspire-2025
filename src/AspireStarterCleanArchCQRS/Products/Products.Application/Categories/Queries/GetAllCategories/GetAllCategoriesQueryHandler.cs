using AutoMapper;
using MediatR;
using Products.Domain.Interfaces.Repositories;
using Products.Shared.DTOs;

namespace Products.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(ICategoriesRepository categoriesRepository, IMapper mapper) : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoriesRepository _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepository.GetAllCategoriesAsync(cancellationToken);

        return _mapper.Map<List<CategoryDto>>(categories);
    }
}