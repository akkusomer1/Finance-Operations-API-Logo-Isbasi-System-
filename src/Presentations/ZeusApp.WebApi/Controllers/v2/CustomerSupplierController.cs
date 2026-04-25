using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.Ayarlar.Queries.GetAllPaged;
using ZeusApp.Application.Features.CorporateCustomerSupplieries.Queries.GetAllPaged;
using ZeusApp.Application.Features.CorporateCustomerSupplieries.Queries.GetById;
using ZeusApp.Application.Features.CustomerSupplieries.Commands.Create;
using ZeusApp.Application.Features.CustomerSupplieries.Commands.Delete;
using ZeusApp.Application.Features.CustomerSupplieries.Commands.Update;
using ZeusApp.Application.Features.CustomerSuppliers.Queries.GetCustomerOrSupplierAdress;
using ZeusApp.Application.Features.CustomerSuppliers.Queries.GetDropdownList;
using ZeusApp.Domain.Enums;
using ZeusApp.Infrastructure.DbContexts;

namespace ZeusApp.WebApi.Controllers.v2;

public class CustomerSupplierController : BaseApiController<CustomerSupplierController>
{
    private readonly ApplicationDbContext _context;

    public CustomerSupplierController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerSupplierCommand command)
    {
        var model = await _mediator.Send(command);
        return Ok(model);
    }



    [HttpPut]
    public async Task<IActionResult> Put(UpdateCustomerSupplierCommand command)
    {
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }

    //CustomerSupplierController Delete endpoint hata var daha sonra incelenecek
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult =await _mediator.Send(new DeleteCustomerSupplierCommand(id));
        return Ok(deleteResult);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {    
        var customerSupplier = await _mediator.Send(new GetCustomerSupplierByIdQuery(id));
        return Ok(customerSupplier);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
    {
        var customerSuppliers = await _mediator.Send(new GetAllCustomerSuppliersQuery(pageNumber, pageSize));
        return Ok(customerSuppliers);
    }

    //Faturalarda müşterileri getirmek için
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllCustomerOrSupplier(InvoiceType invoiceType)
    {
        var customers = await _mediator.Send(new GetAllCustomerOrSupplierQuery { InvoiceType = invoiceType });
        return Ok(customers);
    }


    //Faturalarda müşteri bilgileri için
    [HttpGet("[action]")]
    public async Task<IActionResult> GetCustomerOrSupplierAdressInfo(int customerSupplierId)
    {
        var invoices = await _mediator.Send(new GetCustomerOrSupplierAdressQuery { Id = customerSupplierId });
        return Ok(invoices);
    }

}
