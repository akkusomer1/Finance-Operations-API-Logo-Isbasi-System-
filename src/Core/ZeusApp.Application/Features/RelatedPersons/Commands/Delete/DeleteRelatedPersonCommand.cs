using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.RelatedPersons.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.RelatedPersons.Commands.Delete;
public class DeleteRelatedPersonCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
  
    // ReSharper disable once UnusedMember.Global
    public class DeleteRelatedPersonCommandHandler : IRequestHandler<DeleteRelatedPersonCommand, Result<int>>
    {
        private readonly IRelatedPersonRepository _relatedPersonRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRelatedPersonCommandHandler(IRelatedPersonRepository relatedPersonRepository, IUnitOfWork unitOfWork)
        {
            _relatedPersonRepository = relatedPersonRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteRelatedPersonCommand command, CancellationToken cancellationToken)
        {
            var relatedPerson = await _relatedPersonRepository.GetByIdAsync(command.Id);

            await _relatedPersonRepository.DeleteAsync(relatedPerson);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(relatedPerson.Id, 200);
        }
    }
}
