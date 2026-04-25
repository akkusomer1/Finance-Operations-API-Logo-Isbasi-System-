using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.Holds.Commands.Create;
public class CreateHoldCommand : IRequest<Result<int>>
{

    public string Name { get; set; }

}
public class CreateHoldCommandHandler : IRequestHandler<CreateHoldCommand, Result<int>>
{
    private readonly IHoldRepository _holdRepository;
    private IUnitOfWork _unitOfWork { get; set; }
    private readonly IMapper _mapper;

    public CreateHoldCommandHandler(IHoldRepository holdRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _holdRepository = holdRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateHoldCommand request, CancellationToken cancellationToken)
    {
        var response = _mapper.Map<Hold>(request);
        await _holdRepository.InsertAsync(response);
        await _unitOfWork.Commit(cancellationToken);
        return Result<int>.Success(response.Id, 200);
    }
}
