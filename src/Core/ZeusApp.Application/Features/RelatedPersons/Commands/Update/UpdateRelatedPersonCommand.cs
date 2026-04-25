using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Features.Banks.Commands.Update;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.RelatedPersons.Commands.Update;
public class UpdateRelatedPersonCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public int CustomerSupplierId { get; set; }

    [Display(Name = "Ad Soyad")]
    public string? FullName { get; set; }

    [Display(Name = "Telefon")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "E-posta")]
    public string? Email { get; set; }

    public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankRepository _bankRepository;

        public UpdateBankCommandHandler(IBankRepository bankRepository, IUnitOfWork unitOfWork)
        {
            _bankRepository = bankRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateBankCommand command, CancellationToken cancellationToken)
        {
            var bank = await _bankRepository.GetByIdAsync(command.Id);

            if (bank == null)
            {
                return await Result<int>.FailAsync(404);
            }



            await _bankRepository.UpdateAsync(bank);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(bank.Id, 200);
        }
    }

}
