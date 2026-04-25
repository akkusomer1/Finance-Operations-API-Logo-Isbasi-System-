using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.Ayarlar.Commands.Update;

namespace ZeusApp.Application.Validations.Ayarlar;
public class UpdateAyarValidator : AbstractValidator<UpdateAyarCommand>
{
    public UpdateAyarValidator()
    {
        
    }
}
