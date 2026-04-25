using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.ServiceGroups.Commands.Create;
using ZeusApp.Application.Features.ServiceGroups.Commands.Delete;
using ZeusApp.Application.Features.ServiceGroups.Commands.Update;
using ZeusApp.Application.Features.ServiceGroups.Queries.GetAllPaged;
using ZeusApp.Application.Features.ServiceGroups.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;

public class ServiceGroupController : BaseApiController<ServiceGroupController>
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _mediator.Send(new GetAllServiceGroupsQuery());
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var getById = await _mediator.Send(new GetServiceGroupByIdQuery { Id = id });
        return Ok(getById);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateServiceGroupCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateServiceGroupCommand command)
    {

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteServiceGroupCommand { Id = id });
        return Ok(deleteResult);
    }
}
