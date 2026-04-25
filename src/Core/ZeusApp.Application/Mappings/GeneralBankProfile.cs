using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.GeneralBanks.Commands.Create;
using ZeusApp.Application.Features.GeneralBanks.Commands.Update;
using ZeusApp.Application.Features.GeneralBanks.Queries.GetAllPaged;
using ZeusApp.Application.Features.GeneralBanks.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class GeneralBankProfile:Profile
{
    public GeneralBankProfile()
    {
        CreateMap<CreateGeneralBankCommand, GeneralBank>().ReverseMap();
        CreateMap<UpdateGeneralBankCommand, GeneralBank>().ReverseMap();
        CreateMap<GetAllGeneralBanksResponse, GeneralBank>().ReverseMap();
        CreateMap<GetGeneralBankByIdResponse, GeneralBank>().ReverseMap();
    }
}
