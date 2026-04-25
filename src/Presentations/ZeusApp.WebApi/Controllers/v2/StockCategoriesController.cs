using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.CustomerCategories.Commands.Create;
using ZeusApp.Application.Features.CustomerCategories.Commands.Delete;
using ZeusApp.Application.Features.CustomerCategories.Commands.Update;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetById;
using ZeusApp.Application.Features.StockCategories.Commands.Create;
using ZeusApp.Application.Features.StockCategories.Commands.Delete;
using ZeusApp.Application.Features.StockCategories.Commands.Update;
using ZeusApp.Application.Features.StockCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.StockCategories.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;
public class StockCategoriesController :  BaseApiController<StockCategoriesController>
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customerCategories = await _mediator.Send(new GetAllStockCategoriesQuery());
        return Ok(customerCategories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customerCategory = await _mediator.Send(new GetStockCategoryByIdQuery { Id = id });
        return Ok(customerCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateStockCategoryCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateStockCategoryCommand command)
    {

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteStockCategoryCommand { Id = id });
        return Ok(deleteResult);
    }
}
