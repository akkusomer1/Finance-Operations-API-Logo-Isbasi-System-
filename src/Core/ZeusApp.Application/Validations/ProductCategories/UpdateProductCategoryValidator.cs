using FluentValidation;
using ZeusApp.Application.Features.ProductServiceCategories.Commands.Update;

namespace ZeusApp.Application.Validations.ProductCategories;
public class UpdateProductCategoryValidator : AbstractValidator<UpdateProductServiceCategoryCommand>
{
    public UpdateProductCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
