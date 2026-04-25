using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Services.Queries.GetById;
public class GetServiceByIdQuery : IRequest<Result<GetServiceByIdResponse>>
{
    public int Id { get; set; }
}


public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, Result<GetServiceByIdResponse>>
{
    private readonly IProductServiceRepository _serviceRepository;
    private readonly IMapper _mapper;
    public GetServiceByIdQueryHandler(IProductServiceRepository serviceRepository, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetServiceByIdResponse>> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.ProductServices
              .Where(x => x.ProductServiceType == ProductServiceType.service)
              .SingleOrDefaultAsync(x => x.Id == request.Id);

        var mappedService = _mapper.Map<GetServiceByIdResponse>(service);
        return Result<GetServiceByIdResponse>.Success(mappedService, 200);
    }
}