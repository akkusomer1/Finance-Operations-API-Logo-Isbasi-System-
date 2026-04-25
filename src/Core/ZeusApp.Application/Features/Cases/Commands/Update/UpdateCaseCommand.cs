using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.Banks.Commands.Update;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZeusApp.Application.Features.Cases.Commands.Update;
public class UpdateCaseCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public CurrencyType Currency { get; set; }

    public decimal OpeningBalance { get; set; }

    public DateTime OpeningBalanceDate { get; set; }
    public class UpdateCaseCommandHandler : IRequestHandler<UpdateCaseCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICaseRepository _caseRepository;

        public UpdateCaseCommandHandler(IUnitOfWork unitOfWork, ICaseRepository caseRepository  )
        {
            _unitOfWork = unitOfWork;
            _caseRepository = caseRepository;
        }

        public async Task<Result<int>> Handle(UpdateCaseCommand request, CancellationToken cancellationToken)
        {
            var @case = await _caseRepository.GetByIdAsync(request.Id);

            if (@case == null)
            {
                throw new KeyNotFoundException("Kasa bulunamadı."); 
                
            }
            @case.Name = request.Name;
            @case.Currency = request.Currency;
            @case.OpeningBalance = request.OpeningBalance;
            @case.OpeningBalanceDate = request.OpeningBalanceDate;

            await _caseRepository.UpdateAsync(@case);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(@case.Id, 200);
        }
    }
}
