using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.OtherAddresses.Commands.Create;

namespace ZeusApp.Application.Validations.OtherAddresses;
public class CreateOtherAddressValidator : AbstractValidator<CreateOtherAddressCommand>
{
    public CreateOtherAddressValidator()
    {
        
    }
}
