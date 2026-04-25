using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.Ayarlar.Commands.Create;

namespace ZeusApp.Application.Validations.Ayarlar;
public class CreateAyarValidator : AbstractValidator<CreateAyarCommand>
{
    public CreateAyarValidator()
    {
        
    }
}
