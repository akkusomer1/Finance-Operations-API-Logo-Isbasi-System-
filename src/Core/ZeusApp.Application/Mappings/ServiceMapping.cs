using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.Services.Commands.Create;
using ZeusApp.Application.Features.Services.Commands.Delete;
using ZeusApp.Application.Features.Services.Commands.Update;
using ZeusApp.Application.Features.Services.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class ServiceMapping:Profile
{
    public ServiceMapping()
    {
        CreateMap<ProductService, CreateServiceCommand>().ReverseMap();
        CreateMap<ProductService, UpdateServiceCommand>().ReverseMap();
        CreateMap<GetServiceByIdResponse, ProductService>().ReverseMap();
    }
}
