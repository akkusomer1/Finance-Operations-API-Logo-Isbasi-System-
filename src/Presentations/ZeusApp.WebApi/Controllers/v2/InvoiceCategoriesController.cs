using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.InvoiceCategories.Commands.Create;
using ZeusApp.Application.Features.InvoiceCategories.Commands.Delete;
using ZeusApp.Application.Features.InvoiceCategories.Commands.Update;
using ZeusApp.Application.Features.InvoiceCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.InvoiceCategories.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;
public class InvoiceCategoriesController : BaseApiController<InvoiceCategoriesController>
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var SalesInvoiceCategories = await _mediator.Send(new GetAllInvoiceCategoriesQuery());
        return Ok(SalesInvoiceCategories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var SalesInvoiceCategory = await _mediator.Send(new GetInvoiceCategoryByIdQuery { Id = id });
        return Ok(SalesInvoiceCategory);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateInvoiceCategoryCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateInvoiceCategoryCommand command)
    {

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteInvoiceCategoryCommand { Id = id });
        return Ok(deleteResult);
    }
}
