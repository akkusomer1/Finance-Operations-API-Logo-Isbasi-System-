using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.Expenses.Commands.Create;

namespace ZeusApp.WebApi.Controllers.v2;

public class ExpenseController : BaseApiController<ExpenseController>
{
    [HttpPost]
    public async Task<IActionResult> Post(CreateExpenseCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }



}
