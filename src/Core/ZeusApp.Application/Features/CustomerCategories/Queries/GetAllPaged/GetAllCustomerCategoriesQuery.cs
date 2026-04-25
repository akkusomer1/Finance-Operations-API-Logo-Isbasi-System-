using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using AutoMapper.Internal.Mappers;
using MediatR;
using ZeusApp.Application.Features.Ayarlar.Queries.GetAllPaged;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.CustomerCategories.Queries.GetAllPaged;
public class GetAllCustomerCategoriesQuery : IRequest<Result< List< GetAllCustomerCategoriesResponse>>>
{

}
public class GetAllCustomerCategoriesQueryHandler : IRequestHandler<GetAllCustomerCategoriesQuery, Result<List< GetAllCustomerCategoriesResponse>>>
{
    private readonly ICustomerCategoryRepository _customerCategoryRepository;
    private readonly IMapper _mapper;

    public GetAllCustomerCategoriesQueryHandler(ICustomerCategoryRepository customerCategoryRepository, IMapper mapper)
    {
        _customerCategoryRepository = customerCategoryRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<GetAllCustomerCategoriesResponse>>> Handle(GetAllCustomerCategoriesQuery request, CancellationToken cancellationToken)
    {
        var customerCategories = await _customerCategoryRepository.GetListAsync();
        var customerCategoriesResponse = _mapper.Map<List<GetAllCustomerCategoriesResponse>>(customerCategories);
        return Result<List<GetAllCustomerCategoriesResponse>>.Success(customerCategoriesResponse, 200);
    }
}




