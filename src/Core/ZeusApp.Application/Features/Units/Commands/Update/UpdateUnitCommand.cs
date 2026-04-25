using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.StockCategories.Commands.Update;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Units.Commands.Update;
public class UpdateUnitCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitRepository _unitRepository;

        public UpdateUnitCommandHandler(IUnitOfWork unitOfWork, IUnitRepository unitRepository)
        {
            _unitOfWork = unitOfWork;
            _unitRepository = unitRepository;
        }

        public async Task<Result<int>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var unit = await _unitRepository.GetByIdAsync(request.Id);
            if (unit == null)
            {
                throw new KeyNotFoundException();
            }
            unit.Name = request.Name;
            await _unitRepository.UpdateAsync(unit);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(unit.Id, 200);
        }
    }

}
