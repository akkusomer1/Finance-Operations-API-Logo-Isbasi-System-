using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Ayarlar.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZeusApp.Application.Features.CustomerCategories.Commands.Delete;
public class DeleteCustomerCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public class DeleteCustomerCategoryCommandHandler : IRequestHandler<DeleteCustomerCategoryCommand, Result<int>>
    {
        private readonly ICustomerCategoryRepository _customerCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCategoryCommandHandler(ICustomerCategoryRepository customerCategoryRepository, 
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerCategoryRepository = customerCategoryRepository;
            _unitOfWork = unitOfWork;            
        }
        public async Task<Result<int>> Handle(DeleteCustomerCategoryCommand request, CancellationToken cancellationToken)
        {
            var customerCategory =await _customerCategoryRepository.GetByIdAsync(request.Id);            
             await _customerCategoryRepository.DeleteAsync(customerCategory);
            await _unitOfWork.Commit(cancellationToken);
            return await Result<int>.SuccessAsync(customerCategory.Id, 200);
        }
    }
}
