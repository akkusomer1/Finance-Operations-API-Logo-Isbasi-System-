using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetAllPaged;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ServiceGroups.Queries.GetAllPaged;
public class GetAllServiceGroupsQuery : IRequest<Result<List<GetAllServiceGroupsResponse>>>
{
}
public class GetAllServiceGroupsQueryHandler : IRequestHandler<GetAllServiceGroupsQuery, Result<List<GetAllServiceGroupsResponse>>>
{
    private readonly IServiceGroupRepository _serviceGroupRepository;
    private readonly IMapper _mapper;

    public GetAllServiceGroupsQueryHandler(IServiceGroupRepository serviceGroupRepository, IMapper mapper)
    {
        _serviceGroupRepository = serviceGroupRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllServiceGroupsResponse>>> Handle(GetAllServiceGroupsQuery request, CancellationToken cancellationToken)
    {
        var serviceGroups = await _serviceGroupRepository.GetListAsync();
        var serviceGroupsResponses = _mapper.Map<List<GetAllServiceGroupsResponse>>(serviceGroups);
        return Result<List<GetAllServiceGroupsResponse>>.Success(serviceGroupsResponses, 200);
    }
}
