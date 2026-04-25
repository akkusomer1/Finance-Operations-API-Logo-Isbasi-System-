using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.CustomerSupplieries.Commands.Update;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.CustomerCategories.Commands.Update;
public class UpdateCustomerCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateCustomerCategoryCommandHandler : IRequestHandler<UpdateCustomerCategoryCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerCategoryRepository _customerCategoryRepository;

        public UpdateCustomerCategoryCommandHandler(IUnitOfWork unitOfWork, ICustomerCategoryRepository customerCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _customerCategoryRepository = customerCategoryRepository;
        }

        public async Task<Result<int>> Handle(UpdateCustomerCategoryCommand request, CancellationToken cancellationToken)
        {
            var customerCategory = await _customerCategoryRepository.GetByIdAsync(request.Id);
            if (customerCategory == null)
            {
                throw new KeyNotFoundException();
            }
            customerCategory.Name = request.Name;
            await _customerCategoryRepository.UpdateAsync(customerCategory);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(customerCategory.Id, 200);
        }
    }

}
