using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.InvoiceCategories.Commands.Update;

namespace ZeusApp.Application.Validations.InvoiceCategories;
public class UpdateInvoiceCategoryValidator : AbstractValidator<UpdateInvoiceCategoryCommand>
{
    public UpdateInvoiceCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
