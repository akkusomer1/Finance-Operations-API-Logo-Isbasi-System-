using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.DTOs.BankAccount;
using ZeusApp.Application.Features.BankAccounts.Commands.Create;
using ZeusApp.Application.Features.BankAccounts.Queries.GetAllPaged;
using ZeusApp.Application.Features.BankAccounts.Queries.GetById;
using ZeusApp.Application.Features.Banks.Commands.Create;
using ZeusApp.Application.Features.Banks.Queries.GetAllPaged;
using ZeusApp.Application.Features.Banks.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class BankAccountProfile : Profile
{
    public BankAccountProfile()
    {
        CreateMap<CreateBankAccountCommand, BankAccount>().ReverseMap();
        CreateMap<GetBankAccountByIdResponse, BankAccount>().ReverseMap();
        CreateMap<GetAllBankAccountsResponse, BankAccount>().ReverseMap();

        CreateMap<BankAccountRequest, BankAccount>().ReverseMap();
        CreateMap<BankAccountResponse, BankAccount>().ReverseMap();
    }
}
