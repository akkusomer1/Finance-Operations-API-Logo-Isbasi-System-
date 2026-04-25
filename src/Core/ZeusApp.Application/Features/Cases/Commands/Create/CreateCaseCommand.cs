using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Cases.Commands.Create;
public class CreateCaseCommand : IRequest<Result<int>>
{
    public string Name { get; set; }
   
    public CurrencyType Currency { get; set; }

    public decimal OpeningBalance { get; set; }
   
    public DateTime OpeningBalanceDate { get; set; }
}
public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, Result<int>>
{
    private readonly ICaseRepository _caseRepository;
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork { get; set; }

    public CreateCaseCommandHandler(ICaseRepository caseRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _caseRepository = caseRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        var @case = _mapper.Map<Case>(request);

        await _caseRepository.InsertAsync(@case);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(@case.Id, 200);
    }
}
