using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.Ayarlar.Commands.Create;
using ZeusApp.Application.Features.Banks.Commands.Create;

namespace ZeusApp.Application.Validations.Banks;
public class CreateBankValidator : AbstractValidator<CreateBankCommand>
{
    public CreateBankValidator()
    {
    }
}
