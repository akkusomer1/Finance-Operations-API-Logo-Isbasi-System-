using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.InvoiceCategories.Commands.Delete;
public class DeleteInvoiceCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    [Display(Name = "Kategori Adı")]
    public string Name { get; set; }
    public class DeleteInvoiceCategoryHandler : IRequestHandler<DeleteInvoiceCategoryCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceCategoryRepository _InvoiceCategoryRepository;


        public DeleteInvoiceCategoryHandler(IInvoiceCategoryRepository InvoiceCategoryRepository, IUnitOfWork unitOfWork)
        {
            _InvoiceCategoryRepository = InvoiceCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteInvoiceCategoryCommand command, CancellationToken cancellationToken)
        {
            var InvoiceCategories = await _InvoiceCategoryRepository.GetByIdAsync(command.Id);

            await _InvoiceCategoryRepository.DeleteAsync(InvoiceCategories);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(InvoiceCategories.Id, 200);
        }
    }
}