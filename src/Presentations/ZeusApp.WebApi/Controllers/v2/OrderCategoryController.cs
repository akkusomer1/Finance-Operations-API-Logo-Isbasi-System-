using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.OrderCategories.Commands.Create;
using ZeusApp.Application.Features.OrderCategories.Commands.Delete;
using ZeusApp.Application.Features.OrderCategories.Commands.Update;
using ZeusApp.Application.Features.OrderCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.OrderCategories.Queries.GetById;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ZeusApp.WebApi.Controllers.v2;
public class OrderCategoryController : BaseApiController<OrderCategoryController>
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
    {
        var orderCategories = await _mediator.Send(new GetAllOrderCategoriesQuery(pageNumber, pageSize));
        return Ok(orderCategories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var orderCategory = await _mediator.Send(new GetOrderCategoryByIdQuery { Id = id });
        return Ok(orderCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrderCategoryCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateOrderCategoryCommand command)
    {       
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteOrderCategoryCommand { Id = id });
        return Ok(deleteResult);
    }
}
