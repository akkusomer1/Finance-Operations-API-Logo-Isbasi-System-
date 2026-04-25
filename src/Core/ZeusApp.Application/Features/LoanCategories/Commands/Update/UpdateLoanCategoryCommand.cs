using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.Application.Features.LoanCategories.Commands.Update;
public class UpdateLoanCategoryCommand : IRequest<Result<int>>
{
    public int Id { get; set; }
    [Display(Name = "Kategori")]
    public string Name { get; set; }

    public class UpdateLoanCategoryCommandHandler : IRequestHandler<UpdateLoanCategoryCommand, Result<int>>
    {
        private readonly ILoanCategoryRepository _loanCategoryRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork _unitOfWork { get; set; }

        public UpdateLoanCategoryCommandHandler(ILoanCategoryRepository loanCategoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _loanCategoryRepository = loanCategoryRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateLoanCategoryCommand request, CancellationToken cancellationToken)
        {
            var loanCategory = await _loanCategoryRepository.GetByIdAsync(request.Id);

            if (loanCategory == null)
            {
                return await Result<int>.FailAsync(404);
            }

            loanCategory.Name = request.Name;
            await _loanCategoryRepository.UpdateAsync(loanCategory);
            await _unitOfWork.Commit(cancellationToken);

            return await Result<int>.SuccessAsync(loanCategory.Id, 200);
        }
    }
}
