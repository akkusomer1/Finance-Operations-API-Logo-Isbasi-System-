using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.ProductInvoices.Commands.Update;

namespace ZeusApp.Application.Validations.ProductInvoices;
public class UpdateProductInvoiceValidator : AbstractValidator<UpdateProductInvoiceCommand>
{
    public UpdateProductInvoiceValidator()
    {
        
    }
}
