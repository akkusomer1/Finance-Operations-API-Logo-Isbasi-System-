using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.ProductBrands.Commands.Create;
using ZeusApp.Application.Features.ProductBrands.Queries.GetAllPaged;
using ZeusApp.Application.Features.ProductBrands.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class ProductBrandProfile : Profile
{
    public ProductBrandProfile()
    {
        CreateMap<CreateProductBrandCommand, ProductBrand>().ReverseMap();
        CreateMap<GetProductBrandByIdResponse, ProductBrand>().ReverseMap();
        CreateMap<GetAllProductBrandsResponse, ProductBrand>().ReverseMap();
    }
}
