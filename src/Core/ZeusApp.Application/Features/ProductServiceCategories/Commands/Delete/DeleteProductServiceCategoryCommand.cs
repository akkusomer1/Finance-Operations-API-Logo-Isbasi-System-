using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;

namespace ZeusApp.Application.Features.ProductServiceCategories.Commands.Delete;


public class DeleteProductServiceCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }

    [Display(Name = "Kategori Adı")]
    public string Name { get; set; }
    public class DeleteProductCategoryHandler : IRequestHandler<DeleteProductServiceCategoryCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductServiceCategoryRepository _ProductCategoryRepository;


        public DeleteProductCategoryHandler(IProductServiceCategoryRepository productcategoryRepository, IUnitOfWork unitOfWork)
        {
            _ProductCategoryRepository = productcategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(DeleteProductServiceCategoryCommand command, CancellationToken cancellationToken)
        {
            var productcategories = await _ProductCategoryRepository.GetByIdAsync(command.Id);

            await _ProductCategoryRepository.DeleteAsync(productcategories);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(productcategories.Id, 200);
        }
    }
}