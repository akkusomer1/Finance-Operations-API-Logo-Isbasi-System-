using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.Ayarlar.Commands.Create;
using ZeusApp.Application.Features.Ayarlar.Commands.Delete;
using ZeusApp.Application.Features.Ayarlar.Commands.Update;
using ZeusApp.Application.Features.Ayarlar.Queries.GetAllPaged;
using ZeusApp.Application.Features.Ayarlar.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;

public class AyarController : BaseApiController<AyarController>
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
    {
        var ayarlar = await _mediator.Send(new GetAllAyarlarQuery(pageNumber, pageSize));
        return Ok(ayarlar);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ayar = await _mediator.Send(new GetAyarByIdQuery { Id = id });
        return Ok(ayar);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateAyarCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateAyarCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteAyarCommand { Id = id });
        return Ok(deleteResult);
    }
}
