using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.CustomerCategories.Commands.Create;

namespace ZeusApp.Application.Validations.CustomerCategories;
public class CreateCustomerCategoryValidator : AbstractValidator<CreateCustomerCategoryCommand>
{
    public CreateCustomerCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
