using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.OrderCategories.Queries.GetById;
public class GetOrderCategoryByIdQuery : IRequest<Result<GetOrderCategoryByIdResponse>>
{
    public int Id { get; set; }


    public class GetOrderCategoryByIdQueryHandler : IRequestHandler<GetOrderCategoryByIdQuery, Result<GetOrderCategoryByIdResponse>>
    {
        private readonly IOrderCategoryRepository _orderCategoryRepository;
        private readonly IMapper _mapper;

        public GetOrderCategoryByIdQueryHandler(IOrderCategoryRepository orderCategoryRepository, IMapper mapper)
        {
            _orderCategoryRepository = orderCategoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<GetOrderCategoryByIdResponse>> Handle(GetOrderCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var orderCategory = await _orderCategoryRepository.GetByIdAsync(query.Id);
            var mappedOrderCategory = _mapper.Map<GetOrderCategoryByIdResponse>(orderCategory);

            return await Result<GetOrderCategoryByIdResponse>.SuccessAsync(mappedOrderCategory, 200);
        }
    }
}
