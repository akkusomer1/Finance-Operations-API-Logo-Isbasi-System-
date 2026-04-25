using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.Units.Commands.Create;

namespace ZeusApp.Application.Validations.Units;
public class CreateUnitValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName("Birim").WithMessage("{PropertyName} alanı boş bırakılamaz");
    }
}
