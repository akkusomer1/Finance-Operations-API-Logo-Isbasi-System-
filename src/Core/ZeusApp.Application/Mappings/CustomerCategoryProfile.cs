using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.CustomerCategories.Commands.Create;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class CustomerCategoryProfile : Profile
{
    public CustomerCategoryProfile()
    {
        CreateMap<CreateCustomerCategoryCommand, CustomerCategory>().ReverseMap();
        CreateMap<GetCustomerCategoryByIdResponse, CustomerCategory>().ReverseMap();
        CreateMap<GetAllCustomerCategoriesResponse, CustomerCategory>().ReverseMap();
    }
}
