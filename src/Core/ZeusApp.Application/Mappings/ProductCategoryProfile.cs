using AutoMapper;
using ZeusApp.Application.Features.ProductServiceCategories.Commands.Create;
using ZeusApp.Application.Features.ProductServiceCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.ProductServiceCategories.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class ProductCategoryProfile : Profile
{
    public ProductCategoryProfile()
    {
        CreateMap<CreateProductServiceCategoryCommand, ProductServiceCategory>().ReverseMap();
        CreateMap<GetProductServiceCategoryByIdResponse, ProductServiceCategory>().ReverseMap();
        CreateMap<GetAllProductServiceCategoriesResponse, ProductServiceCategory>().ReverseMap();
    }
}
