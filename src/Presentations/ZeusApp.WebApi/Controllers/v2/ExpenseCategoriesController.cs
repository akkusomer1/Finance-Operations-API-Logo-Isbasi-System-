using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.ExpenseCategories.Commands.Create;
using ZeusApp.Application.Features.ExpenseCategories.Commands.Delete;
using ZeusApp.Application.Features.ExpenseCategories.Commands.Update;
using ZeusApp.Application.Features.ExpenseCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.ExpenseCategories.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;

public class ExpenseCategoriesController : BaseApiController<ExpenseCategoriesController>
{

    [HttpPost]
    public async Task<IActionResult> Post(CreateExpenseCategoryCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateExpenseCategoryCommand command)
    {
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteExpenseCategoryCommand { Id = id });
        return Ok(deleteResult);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _mediator.Send(new GetExpenseCategoryByIdQuery { Id = id });
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customerCategories = await _mediator.Send(new GetAllExpenseCategoriesQuery());
        return Ok(customerCategories);
    }


}
