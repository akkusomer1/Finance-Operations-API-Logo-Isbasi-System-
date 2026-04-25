using AutoMapper;
using ZeusApp.Application.Features.Holds.Commands.Create;
using ZeusApp.Application.Features.Holds.Queries.GetAllPaged;
using ZeusApp.Application.Features.Holds.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class HoldProfile:Profile
{
    public HoldProfile()
    {
        CreateMap<CreateHoldCommand, Hold>().ReverseMap();
        CreateMap<GetHoldByIdResponse, Hold>().ReverseMap();
        CreateMap<GetAllHoldsResponse, Hold>().ReverseMap();
    }
}
