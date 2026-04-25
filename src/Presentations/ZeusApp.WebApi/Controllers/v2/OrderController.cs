using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.Orders.Commands.Create;
using ZeusApp.Application.Features.Orders.Commands.Delete;
using ZeusApp.Application.Features.Orders.Commands.Update;
using ZeusApp.Application.Features.Orders.Queries.GetAllPaged;
using ZeusApp.Application.Features.Orders.Queries.GetById;
using ZeusApp.Domain.Enums;

namespace ZeusApp.WebApi.Controllers.v2;

public class OrderController : BaseApiController<OrderController>
{
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize,InvoiceType invoiceType)
    {
        var orders = await _mediator.Send(new GetAllOrdersQuery(pageNumber, pageSize,x=>x.InvoiceType==invoiceType));
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery { Id = id });
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateOrderCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateOrderCommand command)
    {
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id,InvoiceType invoiceType)
    {
        var deleteResult = await _mediator.Send(new DeleteOrderCommand { Id = id,InvoiceType=invoiceType });
        return Ok(deleteResult);
    }

    //Siparişi faturalama işlemi önce siparişe ait dataları faturaya yansıt.

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> OrderInvoicesGetData(int orderId)
    {
        var order = await _mediator.Send(new GetOrderInvoiceByIdQuery { OrderId = orderId });
        return Ok(order);
    }
}
