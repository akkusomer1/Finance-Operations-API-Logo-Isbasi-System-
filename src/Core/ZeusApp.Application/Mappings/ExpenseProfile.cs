using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.Expenses.Commands.Create;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class ExpenseProfile:Profile
{
    public ExpenseProfile()
    {
        CreateMap<CreateExpenseCommand,Expense>().ReverseMap();
    }
}
