using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.CustomerSupplieries.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.CustomerCategories.Commands.Create;
public class CreateCustomerCategoryCommand : IRequest<Result<int>>
{
    [Required(ErrorMessage = "Müşteri kategori alanı zorunludur.")]
    public string Name { get; set; }
   
}
public class CreateCustomerCategoryCommandHandler : IRequestHandler<CreateCustomerCategoryCommand, Result<int>>
{
    private readonly ICustomerCategoryRepository _customerCategoryRepository;
    private IUnitOfWork _unitOfWork { get; set; }
    private readonly IMapper _mapper;

    public CreateCustomerCategoryCommandHandler(ICustomerCategoryRepository customerCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _customerCategoryRepository = customerCategoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateCustomerCategoryCommand request, CancellationToken cancellationToken)
    {
        var individual = _mapper.Map<CustomerCategory>(request);
        await _customerCategoryRepository.InsertAsync(individual);
        await _unitOfWork.Commit(cancellationToken);
        return Result<int>.Success(individual.Id, 200);
    }
}