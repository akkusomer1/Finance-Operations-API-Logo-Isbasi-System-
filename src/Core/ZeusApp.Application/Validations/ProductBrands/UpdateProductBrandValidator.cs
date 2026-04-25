using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.ProductBrands.Commands.Update;

namespace ZeusApp.Application.Validations.ProductBrands;
public class UpdateProductBrandValidator : AbstractValidator<UpdateProductBrandCommand>
{
    public UpdateProductBrandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
