using AutoMapper;
using MediatR;
using Products.Domain.Interfaces.Repositories;
using Products.Shared.DTOs;

namespace Products.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandlerV2(ICategoriesRepositoryV2 categoriesRepositoryV2, IMapper mapper) : IRequestHandler<GetAllCategoriesQueryV2, List<CategoryDto>>
{
    private readonly ICategoriesRepositoryV2 _categoriesRepositoryV2 = categoriesRepositoryV2 ?? throw new ArgumentNullException(nameof(categoriesRepositoryV2));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQueryV2 request, CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepositoryV2.GetAllCategoriesAsync(cancellationToken);

        return _mapper.Map<List<CategoryDto>>(categories);
    }
}