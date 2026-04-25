using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Holds.Commands.Delete;
public class DeleteHoldCommand : IRequest<Result<int>>
{
    public int Id { get; set; }



}
public class DeleteHoldCommandHandler : IRequestHandler<DeleteHoldCommand, Result<int>>
{
    private readonly IHoldRepository _holdRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHoldCommandHandler(IHoldRepository holdRepository,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _holdRepository = holdRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<int>> Handle(DeleteHoldCommand request, CancellationToken cancellationToken)
    {
        var hold = await _holdRepository.GetByIdAsync(request.Id);
        await _holdRepository.DeleteAsync(hold);
        await _unitOfWork.Commit(cancellationToken);
        return await Result<int>.SuccessAsync(hold.Id, 200);
    }
}
