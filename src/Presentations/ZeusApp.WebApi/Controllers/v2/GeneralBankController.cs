using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.GeneralBanks.Commands.Create;
using ZeusApp.Application.Features.GeneralBanks.Commands.Delete;
using ZeusApp.Application.Features.GeneralBanks.Commands.Update;
using ZeusApp.Application.Features.GeneralBanks.Queries.GetAllPaged;
using ZeusApp.Application.Features.GeneralBanks.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;

public class GeneralBankController : BaseApiController<GeneralBankController>
{
    [HttpPost]
    public async Task<IActionResult> Post(CreateGeneralBankCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteGeneralBankCommand { Id = id });
        return Ok(deleteResult);
    }
    [HttpPut]
    public async Task<IActionResult> Put(UpdateGeneralBankCommand command)
    {
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var generalBanks = await _mediator.Send(new GetAllGeneralBanksQuery());
        return Ok(generalBanks);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var generalBank = await _mediator.Send(new GetGeneralBankByIdQuery { Id = id });
        return Ok(generalBank);
    }
}
