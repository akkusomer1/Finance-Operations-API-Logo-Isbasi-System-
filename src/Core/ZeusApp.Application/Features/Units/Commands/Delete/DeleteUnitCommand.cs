using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.StockCategories.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Units.Commands.Delete;
public class DeleteUnitCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public class DeleteUnitCommandHandler : IRequestHandler<DeleteUnitCommand, Result<int>>
    {
        private readonly IUnitRepository _unitRepository;
        private IUnitOfWork _unitOfWork { get; set; }

        public DeleteUnitCommandHandler(IUnitRepository unitRepository, IUnitOfWork unitOfWork)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.Id);
            await _unitRepository.DeleteAsync(unit);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(unit.Id, 200);
        }
    }
}
