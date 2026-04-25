using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.Cases.Commands.Create;
using ZeusApp.Application.Features.Cases.Commands.Delete;
using ZeusApp.Application.Features.Cases.Commands.Update;
using ZeusApp.Application.Features.Cases.Queries.GetAllPaged;
using ZeusApp.Application.Features.Cases.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;
public class CaseController : BaseApiController<CaseController>
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var @case = await _mediator.Send(new GetCaseByIdQuery { Id = id });
        return Ok(@case);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cases = await _mediator.Send(new GetAllCasesQuery());
        return Ok(cases);
    }
    [HttpPost]
    public async Task<IActionResult> Post(CreateCaseCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateCaseCommand command)
    {

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteCaseCommand { Id = id });
        return Ok(deleteResult);
    }
}
