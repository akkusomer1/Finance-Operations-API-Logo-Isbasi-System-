using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetById;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.StockCategories.Queries.GetById;
public class GetStockCategoryByIdQuery : IRequest<Result<GetStockCategoryByIdResponse>>
{
    public int Id { get; set; }
    public class GetStockCategoryByIdQueryHandler : IRequestHandler<GetStockCategoryByIdQuery, Result<GetStockCategoryByIdResponse>>
    {
        private readonly IStockCategoryRepository _stockCategoryRepository;
        private readonly IMapper _mapper;

        public GetStockCategoryByIdQueryHandler(IStockCategoryRepository stockCategoryRepository, IMapper mapper)
        {
            _stockCategoryRepository = stockCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetStockCategoryByIdResponse>> Handle(GetStockCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var stockCategory = await _stockCategoryRepository.GetByIdAsync(request.Id);
            var getStockCategoryByIdResponse = _mapper.Map<GetStockCategoryByIdResponse>(stockCategory);
            return await Result<GetStockCategoryByIdResponse>.SuccessAsync(getStockCategoryByIdResponse, 200);
        }
    }
}
