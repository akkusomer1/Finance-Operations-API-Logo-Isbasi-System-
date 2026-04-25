using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.OtherAddresses.Commands.Update;
public class UpdateOtherAddressCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    public int CustomerSupplierId { get; set; }
    [Display(Name = "Adres Başlığı")]
    public string? AddressTitle { get; set; }

    [Display(Name = "Ad Soyad")]
    public string? FullName { get; set; }

    [Display(Name = "Telefon")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "Adres")]
    public string? Address { get; set; }

    [Display(Name = "Ülke")]
    public string? Country { get; set; }

    [Display(Name = "İl")]
    public string? City { get; set; }

    [Display(Name = "İlçe")]
    public string? District { get; set; }

    public class UpdateOtherAddressCommandHandler : IRequestHandler<UpdateOtherAddressCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOtherAddressRepository _otherAddressRepository;

        public UpdateOtherAddressCommandHandler(IOtherAddressRepository otherAddressRepository, IUnitOfWork unitOfWork)
        {
            _otherAddressRepository = otherAddressRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateOtherAddressCommand command, CancellationToken cancellationToken)
        {
            var otherAddress = await _otherAddressRepository.GetByIdAsync(command.Id);

            if (otherAddress == null)
            {
                return await Result<int>.FailAsync(404);
            }


            await _otherAddressRepository.UpdateAsync(otherAddress);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(otherAddress.Id, 200);
        }
    }
}
