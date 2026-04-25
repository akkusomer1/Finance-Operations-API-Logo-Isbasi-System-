using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.CorporateCustomerSupplieries.Queries.GetById;
public class GetCustomerSupplierByIdQuery : IRequest<Result<GetCustomerSupplierByIdResponse>>
{
    public int Id { get; set; }

    public GetCustomerSupplierByIdQuery(int ıd)
    {
        Id = ıd;
    }

    public class GetCustomerSupplierByIdQueryHandler : IRequestHandler<GetCustomerSupplierByIdQuery, Result<GetCustomerSupplierByIdResponse>>
    {
        private readonly ICustomerSupplierRepository _repository;
        private readonly IMapper _mapper;

        public GetCustomerSupplierByIdQueryHandler(ICustomerSupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<GetCustomerSupplierByIdResponse>> Handle(GetCustomerSupplierByIdQuery query, CancellationToken cancellationToken)
        {
            var customerSupplier = await _repository.GetByIdAsync(query.Id, true);

            var mappedCustomerSupplier = _mapper.Map<GetCustomerSupplierByIdResponse>(customerSupplier);
          
            return Result<GetCustomerSupplierByIdResponse>.Success(mappedCustomerSupplier, 200);
        }
    }
}
