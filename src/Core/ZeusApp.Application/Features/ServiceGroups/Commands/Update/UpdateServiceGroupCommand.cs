using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Commands.Update;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ServiceGroups.Commands.Update;
public class UpdateServiceGroupCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public class UpdateServiceGroupCommandHandler : IRequestHandler<UpdateServiceGroupCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceGroupRepository _serviceGroupRepository;

        public UpdateServiceGroupCommandHandler(IUnitOfWork unitOfWork, IServiceGroupRepository serviceGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _serviceGroupRepository = serviceGroupRepository;
        }

        public async Task<Result<int>> Handle(UpdateServiceGroupCommand request, CancellationToken cancellationToken)
        {
            var serviceGroup = await _serviceGroupRepository.GetByIdAsync(request.Id);
            if (serviceGroup == null)
            {
                throw new KeyNotFoundException();
            }
            serviceGroup.Name = request.Name;
            await _serviceGroupRepository.UpdateAsync(serviceGroup);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(serviceGroup.Id, 200);
        }
    }

}
