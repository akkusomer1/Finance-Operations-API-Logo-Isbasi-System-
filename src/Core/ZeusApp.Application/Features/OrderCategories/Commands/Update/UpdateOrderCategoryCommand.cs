using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.OrderCategories.Commands.Create;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZeusApp.Application.Features.OrderCategories.Commands.Update;
public class UpdateOrderCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    [Display(Name = "Kategori")]
    public string Name { get; set; }

    public class UpdateOrderCategoryCommandHandler : IRequestHandler<UpdateOrderCategoryCommand, Result<int>>
    {
        private readonly IOrderCategoryRepository _orderCategoryRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateOrderCategoryCommandHandler(IOrderCategoryRepository orderCategoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderCategoryRepository = orderCategoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<int>> Handle(UpdateOrderCategoryCommand command, CancellationToken cancellationToken)
        {
            var ordercategory = await _orderCategoryRepository.GetByIdAsync(command.Id);

            if (ordercategory == null)
            {
                return await Result<int>.FailAsync(404);
            }

            ordercategory.Name = command.Name;
            await _orderCategoryRepository.UpdateAsync(ordercategory);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(ordercategory.Id,200);
        }
    }
}
