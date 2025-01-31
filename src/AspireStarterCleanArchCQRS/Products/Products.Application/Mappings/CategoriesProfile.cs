using AutoMapper;
using Products.Domain.Entities;
using Products.Shared.DTOs;

namespace Products.Application.Mappings;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
