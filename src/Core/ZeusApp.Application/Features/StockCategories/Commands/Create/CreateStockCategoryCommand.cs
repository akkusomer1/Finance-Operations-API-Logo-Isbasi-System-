using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.StockCategories.Commands.Create;
public class CreateStockCategoryCommand : IRequest<Result<int>>
{
    public string Name { get; set; }
}
public class CreateStockCategoryCommandHandler : IRequestHandler<CreateStockCategoryCommand, Result<int>>
{
    private readonly IStockCategoryRepository _stockCategoryRepository;
    private IUnitOfWork _unitOfWork { get; set; }
    private readonly IMapper _mapper;

    public CreateStockCategoryCommandHandler(IStockCategoryRepository stockCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _stockCategoryRepository = stockCategoryRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<int>> Handle(CreateStockCategoryCommand request, CancellationToken cancellationToken)
    {
        var individual = _mapper.Map<StockCategory>(request);
        await _stockCategoryRepository.InsertAsync(individual);
        await _unitOfWork.Commit(cancellationToken);
        return Result<int>.Success(individual.Id, 200);
    }
}

