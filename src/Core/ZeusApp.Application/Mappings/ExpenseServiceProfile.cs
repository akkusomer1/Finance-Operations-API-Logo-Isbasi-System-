using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.DTOs.Expense;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class ExpenseServiceProfile:Profile
{
    public ExpenseServiceProfile()
    {
        CreateMap<ExpenseServiceCreateRequest, ExpenseService>().ReverseMap();
    }
}
