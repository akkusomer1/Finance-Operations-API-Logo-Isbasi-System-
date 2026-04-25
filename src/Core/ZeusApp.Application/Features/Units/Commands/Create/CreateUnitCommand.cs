using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Ayarlar.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using Unit = ZeusApp.Domain.Entities.Catalog.Unit;

namespace ZeusApp.Application.Features.Units.Commands.Create;
public class CreateUnitCommand : IRequest<Result<int>>
{
    public string Name { get; set; }
}
public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, Result<int>>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork { get; set; }

    public CreateUnitCommandHandler(IUnitRepository unitRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitRepository = unitRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        var individual = _mapper.Map<Unit>(request); 
        await _unitRepository.InsertAsync(individual);
        await _unitOfWork.Commit(cancellationToken);
        return Result<int>.Success(individual.Id, 200);
    }
}
