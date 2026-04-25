using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Ayarlar.Queries.GetById;
using ZeusApp.Application.Interfaces.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ZeusApp.Application.Features.CustomerCategories.Queries.GetById;
public class GetCustomerCategoryByIdQuery : IRequest<Result<GetCustomerCategoryByIdResponse>>
{
    public int Id { get; set; }
    public class GetCustomerCategoryByIdQueryHandler : IRequestHandler<GetCustomerCategoryByIdQuery, Result<GetCustomerCategoryByIdResponse>>
    {
        private readonly ICustomerCategoryRepository _customerCategoryRepository;
        private readonly IMapper _mapper;

        public GetCustomerCategoryByIdQueryHandler(ICustomerCategoryRepository customerCategoryRepository, IMapper mapper)
        {
            _customerCategoryRepository = customerCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetCustomerCategoryByIdResponse>> Handle(GetCustomerCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var customerCategory = await _customerCategoryRepository.GetByIdAsync(request.Id);
            var getCustomerCategoryByIdResponse = _mapper.Map<GetCustomerCategoryByIdResponse>(customerCategory);
            return await Result<GetCustomerCategoryByIdResponse>.SuccessAsync(getCustomerCategoryByIdResponse, 200);
        }
    }
}
