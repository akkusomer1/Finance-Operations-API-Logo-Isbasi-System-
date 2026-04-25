using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.StockCategories.Commands.Create;
using ZeusApp.Application.Features.StockCategories.Commands.Delete;
using ZeusApp.Application.Features.StockCategories.Commands.Update;
using ZeusApp.Application.Features.StockCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.StockCategories.Queries.GetById;
using ZeusApp.Application.Features.Units.Commands.Create;
using ZeusApp.Application.Features.Units.Commands.Delete;
using ZeusApp.Application.Features.Units.Commands.Update;
using ZeusApp.Application.Features.Units.Queries.GetAllPaged;
using ZeusApp.Application.Features.Units.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;
public class UnitController : BaseApiController<UnitController>
{

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customerCategories = await _mediator.Send(new GetAllUnitsQuery());
        return Ok(customerCategories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customerCategory = await _mediator.Send(new GetUnitByIdQuery { Id = id });
        return Ok(customerCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateUnitCommand command)
    {
        
        var createResult = await _mediator.Send(command);
        return Ok(createResult);       
        
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateUnitCommand command)
    {

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteUnitCommand { Id = id });
        return Ok(deleteResult);
    }




}
