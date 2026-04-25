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
using ZeusApp.Application.Features.StockCategories.Commands.Create;
using ZeusApp.Application.Features.StockCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.StockCategories.Queries.GetById;
using ZeusApp.Application.Features.Units.Commands.Create;
using ZeusApp.Application.Features.Units.Queries.GetAllPaged;
using ZeusApp.Application.Features.Units.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class UnitProfile : Profile
{
    public UnitProfile()
    {
        CreateMap<CreateUnitCommand, Unit>().ReverseMap();
        CreateMap<GetUnitByIdResponse, Unit>().ReverseMap();
        CreateMap<GetAllUnitsResponse, Unit>().ReverseMap();
    }
}
