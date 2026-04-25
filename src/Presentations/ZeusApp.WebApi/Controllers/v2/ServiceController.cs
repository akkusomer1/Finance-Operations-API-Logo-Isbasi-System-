using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.Services.Commands.Create;
using ZeusApp.Application.Features.Services.Commands.Delete;
using ZeusApp.Application.Features.Services.Commands.Update;
using ZeusApp.Application.Features.Services.Queries.GetAllPaged;
using ZeusApp.Application.Features.Services.Queries.GetById;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.WebApi.Controllers.v2;

public class ServiceController : BaseApiController<ProductService>
{

    [HttpPost]
    public async Task<IActionResult> Post(CreateServiceCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }


    [HttpPut]
    public async Task<IActionResult> Put(UpdateServiceCommand command)
    {
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteServiceCommand { Id = id });
        return Ok(deleteResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var service = await _mediator.Send(new GetServiceByIdQuery { Id = id });
        return Ok(service);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
    {
        var services = await _mediator.Send(new GetAllServicesQuery(pageNumber, pageSize));
        return Ok(services);
    }
}
