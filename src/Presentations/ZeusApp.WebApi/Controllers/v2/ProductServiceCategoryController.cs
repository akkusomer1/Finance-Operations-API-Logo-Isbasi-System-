using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.ProductServiceCategories.Commands.Create;
using ZeusApp.Application.Features.ProductServiceCategories.Commands.Delete;
using ZeusApp.Application.Features.ProductServiceCategories.Commands.Update;
using ZeusApp.Application.Features.ProductServiceCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.ProductServiceCategories.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;
public class ProductServiceCategoryController : BaseApiController<ProductServiceCategoryController>
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var productCategory = await _mediator.Send(new GetProductServiceCategoryByIdQuery { Id = id });
        return Ok(productCategory);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productCategories = await _mediator.Send(new GetAllProductServiceCategoriesQuery());
        return Ok(productCategories);
    }
    [HttpPost]
    public async Task<IActionResult> Post(CreateProductServiceCategoryCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateProductServiceCategoryCommand command)
    {

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteProductServiceCategoryCommand { Id = id });
        return Ok(deleteResult);
    }
}
