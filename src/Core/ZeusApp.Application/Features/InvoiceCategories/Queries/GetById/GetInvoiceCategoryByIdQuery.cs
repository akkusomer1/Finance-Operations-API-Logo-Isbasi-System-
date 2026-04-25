using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.InvoiceCategories.Queries.GetById;
public class GetInvoiceCategoryByIdQuery : IRequest<Result<GetInvoiceCategoryByIdResponse>>
{
    public int Id { get; set; }
    public class GetInvoiceCategoryByIdQueryHandler : IRequestHandler<GetInvoiceCategoryByIdQuery, Result<GetInvoiceCategoryByIdResponse>>
    {
        private readonly IInvoiceCategoryRepository _InvoiceCategoryRepository;
        private readonly IMapper _mapper;

        public GetInvoiceCategoryByIdQueryHandler(IInvoiceCategoryRepository InvoiceCategoryRepository, IMapper mapper)
        {
            _InvoiceCategoryRepository = InvoiceCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetInvoiceCategoryByIdResponse>> Handle(GetInvoiceCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var InvoiceCategory = await _InvoiceCategoryRepository.GetByIdAsync(request.Id);
            var response = _mapper.Map<GetInvoiceCategoryByIdResponse>(InvoiceCategory);
            return await Result<GetInvoiceCategoryByIdResponse>.SuccessAsync(response, 200);
        }
    }
}