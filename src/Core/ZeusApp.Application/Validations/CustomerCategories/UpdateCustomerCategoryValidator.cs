using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.CustomerCategories.Commands.Update;

namespace ZeusApp.Application.Validations.CustomerCategories;
public class UpdateCustomerCategoryValidator : AbstractValidator<UpdateCustomerCategoryCommand>
{
    public UpdateCustomerCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
