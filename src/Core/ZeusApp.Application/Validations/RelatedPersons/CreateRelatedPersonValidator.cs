using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ZeusApp.Application.Features.RelatedPersons.Commands.Create;

namespace ZeusApp.Application.Validations.RelatedPersons;
public class CreateRelatedPersonValidator : AbstractValidator<CreateRelatedPersonCommand>
{
    public CreateRelatedPersonValidator()
    {
        
    }
}
