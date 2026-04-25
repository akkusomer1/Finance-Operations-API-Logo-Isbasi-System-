using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.OtherAddresses.Commands.Create;
public class CreateOtherAddressCommand:IRequest<Result<int>>
{
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
}

public class CreateOtherAddressCommandHandler : IRequestHandler<CreateOtherAddressCommand, Result<int>>
{
    private readonly IOtherAddressRepository _otherAddressRepository;
    private readonly IMapper _mapper;

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    private IUnitOfWork _unitOfWork { get; set; }


    public CreateOtherAddressCommandHandler(IOtherAddressRepository otherAddressRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _otherAddressRepository = otherAddressRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateOtherAddressCommand request, CancellationToken cancellationToken)
    {
        var otherAddress = _mapper.Map<OtherAddress>(request);

        await _otherAddressRepository.InsertAsync(otherAddress);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(otherAddress.Id, 200);
    }
}
