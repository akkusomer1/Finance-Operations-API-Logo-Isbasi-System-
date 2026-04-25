using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ServiceGroups.Commands.Delete;
public class DeleteServiceGroupCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public class DeleteServiceGroupCommandHandler : IRequestHandler<DeleteServiceGroupCommand, Result<int>>
    {
        private readonly IServiceGroupRepository _serviceGroupRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceGroupCommandHandler(IServiceGroupRepository serviceGroupRepository, IUnitOfWork unitOfWork)
        {
            _serviceGroupRepository = serviceGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteServiceGroupCommand request, CancellationToken cancellationToken)
        {
            var serviceGroup = await _serviceGroupRepository.GetByIdAsync(request.Id);
            await _serviceGroupRepository.DeleteAsync(serviceGroup);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(serviceGroup.Id, 200);
        }
    }

}
