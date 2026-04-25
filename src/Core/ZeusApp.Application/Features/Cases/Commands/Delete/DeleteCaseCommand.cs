using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.Banks.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZeusApp.Application.Features.Cases.Commands.Delete;
public class DeleteCaseCommand :  IRequest<Result<int>>
{
    public int Id { get; set; }

    public class DeleteCaseCommandHandler : IRequestHandler<DeleteCaseCommand, Result<int>>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCaseCommandHandler(ICaseRepository caseRepository, IUnitOfWork unitOfWork)
        {
            _caseRepository = caseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteCaseCommand request, CancellationToken cancellationToken)
        {
            var @case = await _caseRepository.GetByIdAsync(request.Id);

            await _caseRepository.DeleteAsync(@case);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(@case.Id, 200);
        }
    }

}
