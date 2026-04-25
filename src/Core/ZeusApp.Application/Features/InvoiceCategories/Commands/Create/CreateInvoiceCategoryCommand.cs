using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.InvoiceCategories.Commands.Create;
public class CreateInvoiceCategoryCommand : IRequest<Result<int>>
{

    [Display(Name = "Kategori Adı")]
    public string Name { get; set; }
}
public class CreateInvoiceCategoryHandler : IRequestHandler<CreateInvoiceCategoryCommand, Result<int>>
{
    private readonly IInvoiceCategoryRepository _InvoiceCategoryRepository;
    private readonly IMapper _mapper;

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    private IUnitOfWork _unitOfWork { get; set; }


    public CreateInvoiceCategoryHandler(IInvoiceCategoryRepository InvoiceCategoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _InvoiceCategoryRepository = InvoiceCategoryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(CreateInvoiceCategoryCommand request, CancellationToken cancellationToken)
    {
        var InvoiceCategories = _mapper.Map<InvoiceCategory>(request);

        await _InvoiceCategoryRepository.InsertAsync(InvoiceCategories);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(InvoiceCategories.Id, 200);
    }
}