using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.ExpenseCategories.Commands.Create;
using ZeusApp.Application.Features.ExpenseCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.ExpenseCategories.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class ExpenseCategoryProfile : Profile
{
    public ExpenseCategoryProfile()
    {
        CreateMap<CreateExpenseCategoryCommand, ExpenseCategory>().ReverseMap();
        CreateMap<GetExpenseCategoryByIdResponse, ExpenseCategory>().ReverseMap();
        CreateMap<GetAllExpenseCategoriesResponse, ExpenseCategory>().ReverseMap();
    }
}
