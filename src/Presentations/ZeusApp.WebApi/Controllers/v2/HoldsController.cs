using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.Holds.Commands.Create;
using ZeusApp.Application.Features.Holds.Commands.Delete;
using ZeusApp.Application.Features.Holds.Commands.Update;
using ZeusApp.Application.Features.Holds.Queries.GetAllPaged;
using ZeusApp.Application.Features.Holds.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;
public class HoldsController : BaseApiController<HoldsController>
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var holds = await _mediator.Send(new GetAllHoldsQuery());
        return Ok(holds);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var hold = await _mediator.Send(new GetHoldByIdQuery { Id = id });
        return Ok(hold);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateHoldCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateHoldCommand command)
    {

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteHoldCommand { Id = id });
        return Ok(deleteResult);
    }
}
