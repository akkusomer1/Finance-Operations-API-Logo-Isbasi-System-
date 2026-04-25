using FluentValidation;
using ZeusApp.Application.Features.ProductServiceCategories.Commands.Create;

namespace ZeusApp.Application.Validations.ProductCategories;
public class CreateProductCategoryValidator : AbstractValidator<CreateProductServiceCategoryCommand>
{
    public CreateProductCategoryValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
