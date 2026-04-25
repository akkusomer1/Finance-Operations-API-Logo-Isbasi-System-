using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Holds.Commands.Update;
public class UpdateHoldCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateHoldCommandHandler : IRequestHandler<UpdateHoldCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHoldRepository _holdRepository;

        public UpdateHoldCommandHandler(IUnitOfWork unitOfWork, IHoldRepository holdRepository)
        {
            _unitOfWork = unitOfWork;
            _holdRepository = holdRepository;
        }

        public async Task<Result<int>> Handle(UpdateHoldCommand command, CancellationToken cancellationToken)
        {
            var hold = await _holdRepository.GetByIdAsync(command.Id);
            if (hold == null)
            {
                throw new KeyNotFoundException();
            }
            hold.Name = command.Name;
            await _holdRepository.UpdateAsync(hold);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(hold.Id, 200);
        }
    }

}