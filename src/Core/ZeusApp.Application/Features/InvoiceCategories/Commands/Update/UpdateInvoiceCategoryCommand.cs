using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.InvoiceCategories.Commands.Update;

public class UpdateInvoiceCategoryCommand : IRequest<Result<int>>
{


    public int Id { get; set; }

    [Display(Name = "Kategori Adı")]
    public string Name { get; set; }
}
public class UpdateInvoiceCategoryCommandHandler : IRequestHandler<UpdateInvoiceCategoryCommand, Result<int>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvoiceCategoryRepository _InvoiceCategoryRepository;

    public UpdateInvoiceCategoryCommandHandler(IInvoiceCategoryRepository InvoiceCategoryRepository, IUnitOfWork unitOfWork)
    {
        _InvoiceCategoryRepository = InvoiceCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(UpdateInvoiceCategoryCommand command, CancellationToken cancellationToken)
    {
        var InvoiceCategories = await _InvoiceCategoryRepository.GetByIdAsync(command.Id);

        if (InvoiceCategories == null)
        {
            throw new KeyNotFoundException();
        }
        InvoiceCategories.Name = command.Name;
        await _InvoiceCategoryRepository.UpdateAsync(InvoiceCategories);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(InvoiceCategories.Id, 200);
    }
}