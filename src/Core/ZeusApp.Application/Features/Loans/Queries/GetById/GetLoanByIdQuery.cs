using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Loans.Queries.GetById;
public class GetLoanByIdQuery : IRequest<Result<GetLoanByIdResponse>>
{
    public int Id { get; set; }

    public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, Result<GetLoanByIdResponse>>
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public GetLoanByIdQueryHandler(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<Result<GetLoanByIdResponse>> Handle(GetLoanByIdQuery query, CancellationToken cancellationToken)
        {
            var loan = await _loanRepository.GetByIdAsync(query.Id);
            var mappedLoan = _mapper.Map<GetLoanByIdResponse>(loan);

            return await Result<GetLoanByIdResponse>.SuccessAsync(mappedLoan, 200);
        }
    }
}