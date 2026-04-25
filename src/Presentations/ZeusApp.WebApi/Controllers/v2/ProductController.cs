using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.CustomerSuppliers.Queries.GetDropdownList;
using ZeusApp.Application.Features.Products.Commands.Create;
using ZeusApp.Application.Features.Products.Commands.Delete;
using ZeusApp.Application.Features.Products.Commands.Update;
using ZeusApp.Application.Features.Products.Queries.GetAllPaged;
using ZeusApp.Application.Features.Products.Queries.GetById;
using ZeusApp.Application.Features.Products.Queries.GetDropdownList;
using ZeusApp.Domain.Enums;

namespace ZeusApp.WebApi.Controllers.v2;

public class ProductControllers : BaseApiController<ProductControllers>
{

    [HttpPost]
    public async Task<IActionResult> Post(CreateProductCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateProductCommand command)
    {
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteProductCommand { Id = id });
        return Ok(deleteResult);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
        return Ok(product);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize, ProductServiceType productServiceType)
    {
        var products = await _mediator.Send(new GetAllProductsQuery(pageNumber, pageSize,x=>x.ProductServiceType==productServiceType));
        return Ok(products);
    }

    //Faturalarda ürünleri ve hizmetleri getirmek için
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllProductsAndServices(SalesInvoiceType salesInvoiceType)
    {
        var customers = await _mediator.Send(new GetAllProductsAndServicesQuery { SalesInvoiceType = salesInvoiceType });
        return Ok(customers);
    }
}
