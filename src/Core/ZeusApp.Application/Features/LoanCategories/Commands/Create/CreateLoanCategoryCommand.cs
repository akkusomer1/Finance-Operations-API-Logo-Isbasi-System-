using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.LoanCategories.Commands.Create;
public class CreateLoanCategoryCommand : IRequest<Result<int>>
{
    [Display(Name = "Kategori")]
    public string Name { get; set; }

}

public class CreateLoanCategoryCommandHandler : IRequestHandler<CreateLoanCategoryCommand, Result<int>>
{
    private readonly ILoanCategoryRepository _loanCategoryRepository;
    private readonly IMapper _mapper;
    private IUnitOfWork _unitOfWork { get; set; }

    public CreateLoanCategoryCommandHandler(ILoanCategoryRepository loanCategoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _loanCategoryRepository = loanCategoryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<int>> Handle(CreateLoanCategoryCommand request, CancellationToken cancellationToken)
    {
        var loanCategory= _mapper.Map<LoanCategory>(request);

        await _loanCategoryRepository.InsertAsync(loanCategory);
        await _unitOfWork.Commit(cancellationToken);

        return await Result<int>.SuccessAsync(loanCategory.Id,200);
    }
}
