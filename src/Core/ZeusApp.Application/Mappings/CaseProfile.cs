using AutoMapper;
using ZeusApp.Application.Features.Cases.Commands.Create;
using ZeusApp.Application.Features.Cases.Commands.Update;
using ZeusApp.Application.Features.Cases.Queries.GetAllPaged;
using ZeusApp.Application.Features.Cases.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class CaseProfile : Profile
{
    public CaseProfile()
    {
        CreateMap<CreateCaseCommand, Case>().ReverseMap();
        CreateMap<GetCaseByIdResponse, Case>().ReverseMap();
        CreateMap<GetAllCasesResponse, Case>().ReverseMap();
    }
}
