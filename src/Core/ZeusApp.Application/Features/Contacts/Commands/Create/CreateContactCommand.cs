using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.Contacts.Commands.Create;

public class CreateContactCommand : IRequest<Result<int>>
{
    public int CustomerSupplierId { get; set; }

    [Display(Name = "Adres")]
    public string? Address { get; set; }

    [Display(Name = "Ülke")]
    public string? Country { get; set; }

    [Display(Name = "İl")]
    public string? City { get; set; }

    [Display(Name = "İlçe")]
    public string? District { get; set; }

    [Display(Name = "Posta Kodu")]
    public string? PostalCode { get; set; }

    [Display(Name = "E-posta")]
    public string? Email { get; set; }

    [Display(Name = "Fax")]
    public string? Fax { get; set; }

    [Display(Name = "Telefon-1")]
    public string? PhoneNumber1 { get; set; }

    [Display(Name = "Telefon-2")]
    public string? PhoneNumber2 { get; set; }

    [Display(Name = "Web Sitesi")]
    public string? Website { get; set; }
}

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Result<int>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    private IUnitOfWork _unitOfWork { get; set; }

    public CreateContactCommandHandler(IContactRepository contactRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = _mapper.Map<Contact>(request);

        await _contactRepository.InsertAsync(contact);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(contact.Id, 200);
    }
}