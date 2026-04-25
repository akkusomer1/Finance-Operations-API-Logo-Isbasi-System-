using AutoMapper;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Application.Features.Ayarlar.Commands.Create;
using ZeusApp.Application.Features.Ayarlar.Queries.GetAllPaged;
using ZeusApp.Application.Features.Ayarlar.Queries.GetById;

namespace ZeusApp.Application.Mappings;

internal class AyarProfile : Profile
{
    public AyarProfile()
    {
        CreateMap<CreateAyarCommand, Ayar>().ReverseMap();
        CreateMap<GetAyarByIdResponse, Ayar>().ReverseMap();
        CreateMap<GetAllAyarlarResponse, Ayar>().ReverseMap();
    }
}
