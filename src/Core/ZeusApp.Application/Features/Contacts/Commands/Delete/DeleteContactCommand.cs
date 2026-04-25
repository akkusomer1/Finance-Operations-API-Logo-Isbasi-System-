using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Contacts.Commands.Delete;
public class DeleteContactCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Result<int>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteContactCommand command, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(command.Id);

            await _contactRepository.DeleteAsync(contact);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(contact.Id, 200);
        }
    }
}
