using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.CarrierCompanies.Commands.Create;
using ZeusApp.Application.Features.CarrierCompanies.Commands.Delete;
using ZeusApp.Application.Features.CarrierCompanies.Commands.Update;
using ZeusApp.Application.Features.CarrierCompanies.Queries.GetAllPaged;
using ZeusApp.Application.Features.CarrierCompanies.Queries.GetById;
using ZeusApp.Application.Features.CustomerCategories.Commands.Create;
using ZeusApp.Application.Features.CustomerCategories.Commands.Delete;
using ZeusApp.Application.Features.CustomerCategories.Commands.Update;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.CustomerCategories.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;
public class CarrierCompaniesController : BaseApiController<CarrierCompaniesController>
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var carrierCompanies = await _mediator.Send(new GetAllCarrierCompaniesQuery());
        return Ok(carrierCompanies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var carrierCompany = await _mediator.Send(new GetCarrierCompanyByIdQuery { Id = id });
        return Ok(carrierCompany);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCarrierCompanyCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateCarrierCompanyCommand command)
    {

        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteCarrierCompanyCommand { Id = id });
        return Ok(deleteResult);
    }
}
