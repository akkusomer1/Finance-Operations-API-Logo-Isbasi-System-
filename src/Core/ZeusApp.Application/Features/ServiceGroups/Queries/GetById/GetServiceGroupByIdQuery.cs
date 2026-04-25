using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetById;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ServiceGroups.Queries.GetById;
public class GetServiceGroupByIdQuery : IRequest<Result<GetServiceGroupByIdResponse>>
{
    public int Id { get; set; }
    public class GetServiceGroupByIdQueryHandler : IRequestHandler<GetServiceGroupByIdQuery, Result<GetServiceGroupByIdResponse>>
    {
        private readonly IServiceGroupRepository _serviceGroupRepository;
        private readonly IMapper _mapper;

        public GetServiceGroupByIdQueryHandler(IServiceGroupRepository serviceGroupRepository, IMapper mapper)
        {
            _serviceGroupRepository = serviceGroupRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetServiceGroupByIdResponse>> Handle(GetServiceGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var serviceGroup = await _serviceGroupRepository.GetByIdAsync(request.Id);
            var serviceGroupByIdResponse = _mapper.Map<GetServiceGroupByIdResponse>(serviceGroup);
            return await Result<GetServiceGroupByIdResponse>.SuccessAsync(serviceGroupByIdResponse, 200);
        }
    }
}
