using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ProductServiceCategories.Queries.GetById;
public class GetProductServiceCategoryByIdQuery : IRequest<Result<GetProductServiceCategoryByIdResponse>>
{
    public int Id { get; set; }
    public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductServiceCategoryByIdQuery, Result<GetProductServiceCategoryByIdResponse>>
    {
        private readonly IProductServiceCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public GetProductCategoryByIdQueryHandler(IProductServiceCategoryRepository productCategoryRepository, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetProductServiceCategoryByIdResponse>> Handle(GetProductServiceCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var customerCategory = await _productCategoryRepository.GetByIdAsync(request.Id);
            var getCustomerCategoryByIdResponse = _mapper.Map<GetProductServiceCategoryByIdResponse>(customerCategory);
            return await Result<GetProductServiceCategoryByIdResponse>.SuccessAsync(getCustomerCategoryByIdResponse, 200);
        }
    }
}