using AutoMapper;
using ZeusApp.Application.Features.LoanCategories.Commands.Create;
using ZeusApp.Application.Features.LoanCategories.Commands.Update;
using ZeusApp.Application.Features.LoanCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.LoanCategories.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class LoanCategoryProfile : Profile
{
    public LoanCategoryProfile()
    {
        CreateMap<CreateLoanCategoryCommand, LoanCategory>().ReverseMap();
        CreateMap<UpdateLoanCategoryCommand, LoanCategory>().ReverseMap();
        CreateMap<GetLoanCategoryByIdResponse, LoanCategory>().ReverseMap();
        CreateMap<GetAllLoanCategoriesResponse, LoanCategory>().ReverseMap();
    }
}
