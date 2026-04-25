using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.ServiceGroups.Commands.Create;
public class CreateServiceGroupCommand : IRequest<Result<int>>
{
    public string Name { get; set; }
}
public class CreateServiceGroupCommandHandler : IRequestHandler<CreateServiceGroupCommand, Result<int>>
{
    private readonly IServiceGroupRepository _serviceGroupRepository;
    private IUnitOfWork _unitOfWork { get; set; }
    private readonly IMapper _mapper;

    public CreateServiceGroupCommandHandler(IServiceGroupRepository serviceGroupRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _serviceGroupRepository = serviceGroupRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateServiceGroupCommand request, CancellationToken cancellationToken)
    {
        var individual = _mapper.Map<ServiceGroup>(request);
        await _serviceGroupRepository.InsertAsync(individual);
        await _unitOfWork.Commit(cancellationToken);
        return Result<int>.Success(individual.Id, 200);
    }
}
