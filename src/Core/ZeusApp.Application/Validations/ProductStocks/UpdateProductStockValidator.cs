using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.ProductStocks.Commands.Update;

namespace ZeusApp.Application.Validations.ProductStocks;
public class UpdateProductStockValidator : AbstractValidator<UpdateProductStockCommand>
{
    public UpdateProductStockValidator()
    {
        
    }
}
