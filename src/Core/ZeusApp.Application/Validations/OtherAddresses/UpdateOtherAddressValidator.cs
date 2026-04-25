using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.OtherAddresses.Commands.Update;

namespace ZeusApp.Application.Validations.OtherAddresses;
public class UpdateOtherAddressValidator : AbstractValidator<UpdateOtherAddressCommand>
{
    public UpdateOtherAddressValidator()
    {
        
    }
}
