using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.StockCategories.Commands.Create;

namespace ZeusApp.Application.Validations.StockCategories;
public class CreateStockCategoryValidator : AbstractValidator<CreateStockCategoryCommand>
{
    public CreateStockCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
