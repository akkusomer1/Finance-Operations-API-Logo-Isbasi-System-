using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.Loans.Commands.Create;
using ZeusApp.Application.Features.Loans.Commands.Delete;
using ZeusApp.Application.Features.Loans.Commands.Update;
using ZeusApp.Application.Features.Loans.Queries.GetAllPaged;
using ZeusApp.Application.Features.Loans.Queries.GetById;

namespace ZeusApp.WebApi.Controllers.v2;
public class LoanController : BaseApiController<LoanController>
{
    [HttpPost]
    public async Task<IActionResult> Post(CreateLoanCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateLoanCommand command)
    {
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteLoanCommand { Id = id });
        return Ok(deleteResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var loan = await _mediator.Send(new GetLoanByIdQuery { Id = id });
        return Ok(loan);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
    {
        var loans = await _mediator.Send(new GetAllLoansQuery(pageNumber, pageSize));
        return Ok(loans);
    }

}
