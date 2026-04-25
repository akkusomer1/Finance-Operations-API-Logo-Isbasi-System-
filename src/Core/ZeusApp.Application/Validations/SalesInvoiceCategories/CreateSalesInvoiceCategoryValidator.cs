using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.InvoiceCategories.Commands.Create;

namespace ZeusApp.Application.Validations.InvoiceCategories;
public class CreateInvoiceCategoryValidator : AbstractValidator<CreateInvoiceCategoryCommand>
{
    public CreateInvoiceCategoryValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
