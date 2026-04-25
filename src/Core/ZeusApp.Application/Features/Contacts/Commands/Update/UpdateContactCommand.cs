using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.Contacts.Commands.Update;

public class UpdateContactCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
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

    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContactRepository _contactRepository;

        public UpdateContactCommandHandler(IContactRepository contactRepository, IUnitOfWork unitOfWork)
        {
            _contactRepository = contactRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateContactCommand command, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetByIdAsync(command.Id);

            if (contact == null)
            {
                return await Result<int>.FailAsync(404);
            }

            await _contactRepository.UpdateAsync(contact);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(contact.Id, 200);
        }
    }
}


