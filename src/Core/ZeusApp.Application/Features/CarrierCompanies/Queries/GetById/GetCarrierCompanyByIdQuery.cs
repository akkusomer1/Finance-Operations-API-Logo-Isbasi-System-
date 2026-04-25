using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetById;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.CarrierCompanies.Queries.GetById;
public class GetCarrierCompanyByIdQuery : IRequest<Result<GetCarrierCompanyByIdResponse>>
{
    public int Id { get; set; }
    public class GetCarrierCompanyByIdQueryHandler : IRequestHandler<GetCarrierCompanyByIdQuery, Result<GetCarrierCompanyByIdResponse>>
    {
        private readonly ICarrierCompanyRepository _carrierCompanyRepository;
        private readonly IMapper _mapper;

        public GetCarrierCompanyByIdQueryHandler(ICarrierCompanyRepository carrierCompanyRepository, IMapper mapper)
        {
            _carrierCompanyRepository = carrierCompanyRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetCarrierCompanyByIdResponse>> Handle(GetCarrierCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var carrierCompany = await _carrierCompanyRepository.GetByIdAsync(request.Id);
            var getCarrierCompanyByIdResponse = _mapper.Map<GetCarrierCompanyByIdResponse>(carrierCompany);
            return await Result<GetCarrierCompanyByIdResponse>.SuccessAsync(getCarrierCompanyByIdResponse, 200);
        }
    }
}