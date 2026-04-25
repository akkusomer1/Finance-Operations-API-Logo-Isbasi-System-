using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.LoanCategories.Commands.Create;
using ZeusApp.Application.Features.LoanCategories.Commands.Delete;
using ZeusApp.Application.Features.LoanCategories.Commands.Update;
using ZeusApp.Application.Features.LoanCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.LoanCategories.Queries.GetById;
using ZeusApp.Application.Features.Services.Commands.Update;
using ZeusApp.Domain.Entities.Catalog;

namespace ZeusApp.WebApi.Controllers.v2;
public class LoanCategoryController : BaseApiController<LoanCategoryController>
{
    [HttpPost]
    public async Task<IActionResult> Post(CreateLoanCategoryCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateLoanCategoryCommand command)
    {
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteLoanCategoryCommand { Id = id });
        return Ok(deleteResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var loanCategory = await _mediator.Send(new GetLoanCategoryByIdQuery { Id = id });
        return Ok(loanCategory);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
    {
        var loanCategories = await _mediator.Send(new GetAllLoanCategoriesQuery(pageNumber, pageSize));
        return Ok(loanCategories);
    }
}
