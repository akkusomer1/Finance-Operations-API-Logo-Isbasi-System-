using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.CarrierCompanies.Queries.GetAllPaged;

public class GetAllCarrierCompaniesQuery : IRequest<Result<List<GetAllCarrierCompaniesResponse>>>
{

}
public class GetAllCarrierCompaniesHandler : IRequestHandler<GetAllCarrierCompaniesQuery, Result<List<GetAllCarrierCompaniesResponse>>>
{
    private readonly ICarrierCompanyRepository _carrierCompanyRepository;
    private readonly IMapper _mapper;

    public GetAllCarrierCompaniesHandler(ICarrierCompanyRepository carrierCompanyRepository, IMapper mapper)
    {
        _carrierCompanyRepository = carrierCompanyRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<GetAllCarrierCompaniesResponse>>> Handle(GetAllCarrierCompaniesQuery request, CancellationToken cancellationToken)
    {
        var carrierCompanies = await _carrierCompanyRepository.GetListAsync();
        var carrierCompaniesResponse = _mapper.Map<List<GetAllCarrierCompaniesResponse>>(carrierCompanies);
        return Result<List<GetAllCarrierCompaniesResponse>>.Success(carrierCompaniesResponse, 200);
    }
}
