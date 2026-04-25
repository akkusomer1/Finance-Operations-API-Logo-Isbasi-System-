using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.DTOs.Bank;
using ZeusApp.Application.Features.Banks.Commands.Create;
using ZeusApp.Application.Features.Banks.Queries.GetAllPaged;
using ZeusApp.Application.Features.Banks.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class BankProfile : Profile
{
    public BankProfile()
    {
        CreateMap<CreateBankCommand, Bank>().ReverseMap();
        CreateMap<GetBankByIdResponse, Bank>().ReverseMap();
        CreateMap<GetAllBanksResponse, Bank>().ReverseMap();

        CreateMap<BankRequest, Bank>().ReverseMap();
        CreateMap<BankResponse, Bank>().ReverseMap();
    }
}
