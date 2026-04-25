using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.Loans.Queries.GetById;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.LoanCategories.Queries.GetById;
public class GetLoanCategoryByIdQuery : IRequest<Result<GetLoanCategoryByIdResponse>>
{
    public int Id { get; set; }

    public class GetLoanCategoryByIdQueryHandler : IRequestHandler<GetLoanCategoryByIdQuery, Result<GetLoanCategoryByIdResponse>>
    {
        private readonly ILoanCategoryRepository _loanCategoryRepository;
        private readonly IMapper _mapper;

        public GetLoanCategoryByIdQueryHandler(ILoanCategoryRepository loanCategoryRepository, IMapper mapper)
        {
            _loanCategoryRepository = loanCategoryRepository;
            _mapper = mapper;
        }
        public async Task<Result<GetLoanCategoryByIdResponse>> Handle(GetLoanCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var loanCategory = await _loanCategoryRepository.GetByIdAsync(query.Id);
            var mappedLoanCategory = _mapper.Map<GetLoanCategoryByIdResponse>(loanCategory);

            return await Result<GetLoanCategoryByIdResponse>.SuccessAsync(mappedLoanCategory, 200);
    }
    }
}
