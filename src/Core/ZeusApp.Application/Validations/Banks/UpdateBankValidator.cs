

using FluentValidation;
using ZeusApp.Application.Features.Banks.Commands.Update;

namespace ZeusApp.Application.Validations.Banks;
public class UpdateBankValidator : AbstractValidator<UpdateBankCommand>
{
    public UpdateBankValidator()
    {
        
    }
}
