using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.ProductBrands.Commands.Create;
using ZeusApp.Application.Features.ProductBrands.Commands.Delete;
using ZeusApp.Application.Features.ProductBrands.Commands.Update;
using ZeusApp.Application.Features.ProductBrands.Queries.GetAllPaged;
using ZeusApp.Application.Features.ProductBrands.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;
public class ProductBrandsController : BaseApiController<ProductBrandsController>
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ProductBrands = await _mediator.Send(new GetAllProductBrandsQuery());
        return Ok(ProductBrands);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ProductBrands = await _mediator.Send(new GetProductBrandByIdQuery { Id = id });
        return Ok(ProductBrands);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProductBrandCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateProductBrandCommand command)
    {

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteProductBrandCommand { Id = id });
        return Ok(deleteResult);
    }
}
