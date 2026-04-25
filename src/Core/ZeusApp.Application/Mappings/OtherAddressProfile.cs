using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.DTOs.Bank;
using ZeusApp.Application.DTOs.Mail;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Application.Features.OtherAddresses.Commands.Create;
using ZeusApp.Application.Features.OtherAddresses.Queries.GetAllPaged;
using ZeusApp.Application.Features.OtherAddresses.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
internal class OtherAddressProfile : Profile
{
    public OtherAddressProfile()
    {
        CreateMap<CreateOtherAddressCommand, OtherAddress>().ReverseMap();
        CreateMap<GetOtherAddressByIdResponse, OtherAddress>().ReverseMap();
        CreateMap<GetAllOtherAddressesResponse, OtherAddress>().ReverseMap();

        CreateMap<OtherAddressResponse, OtherAddress>().ReverseMap();
        CreateMap<OtherAddressRequest, OtherAddress>().ReverseMap();
    }
}
