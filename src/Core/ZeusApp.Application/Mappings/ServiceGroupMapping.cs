using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ZeusApp.Application.Features.CustomerCategories.Commands.Create;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetById;
using ZeusApp.Application.Features.ServiceGroups.Commands.Create;
using ZeusApp.Application.Features.ServiceGroups.Queries.GetAllPaged;
using ZeusApp.Application.Features.ServiceGroups.Queries.GetById;
using ZeusApp.Application.Features.Services.Commands.Create;
using ZeusApp.Application.Features.Services.Commands.Update;
using ZeusApp.Application.Features.Services.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Mappings;
public class ServiceGroupMapping : Profile
{
    public ServiceGroupMapping()
    {
        CreateMap<CreateServiceGroupCommand, ServiceGroup>().ReverseMap();
        CreateMap<GetServiceGroupByIdResponse, ServiceGroup>().ReverseMap();
        CreateMap<GetAllServiceGroupsResponse, ServiceGroup>().ReverseMap();
    }

}
