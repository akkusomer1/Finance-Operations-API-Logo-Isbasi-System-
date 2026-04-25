using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.RelatedPersons.Commands.Create;
public class CreateRelatedPersonCommand:IRequest<Result<int>>
{
    public int CustomerSupplierId { get; set; }

    [Display(Name = "Ad Soyad")]
    public string? FullName { get; set; }

    [Display(Name = "Telefon")]
    public string? PhoneNumber { get; set; }

    [Display(Name = "E-posta")]
    public string? Email { get; set; }
}
public class CreateRelatedPersonCommandHandler : IRequestHandler<CreateRelatedPersonCommand, Result<int>>
{
    private readonly IRelatedPersonRepository _relatedPersonRepository;
    private readonly IMapper _mapper;

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    private IUnitOfWork _unitOfWork { get; set; }


    public CreateRelatedPersonCommandHandler(IRelatedPersonRepository relatedPersonRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _relatedPersonRepository = relatedPersonRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateRelatedPersonCommand request, CancellationToken cancellationToken)
    {
        var relatedPerson = _mapper.Map<RelatedPerson>(request);

        await _relatedPersonRepository.InsertAsync(relatedPerson);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(relatedPerson.Id, 200);
    }
}