using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.ServiceGroups.Commands.Create;
using ZeusApp.Application.Features.StockCategories.Commands.Create;

namespace ZeusApp.Application.Validations.ServiceGroups;
public class CreateServiceGroupValidator : AbstractValidator<CreateServiceGroupCommand>
{
    public CreateServiceGroupValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
