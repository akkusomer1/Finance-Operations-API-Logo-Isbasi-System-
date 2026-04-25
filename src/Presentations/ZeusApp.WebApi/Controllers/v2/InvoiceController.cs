using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.Features.InvoiceCategories.Queries.GetById;
using ZeusApp.Application.Features.Invoices.Commands.Create;
using ZeusApp.Application.Features.Invoices.Commands.Delete;
using ZeusApp.Application.Features.Invoices.Commands.Update;
using ZeusApp.Application.Features.Invoices.Queries.GetAllPaged;
using ZeusApp.Domain.Enums;

namespace ZeusApp.WebApi.Controllers.v2;


public class InvoiceController : BaseApiController<InvoiceController>
{
    [HttpPost]
    public async Task<IActionResult> Post(CreateInvoiceCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateInvoiceCommand command)
    {        
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id,InvoiceType invoiceType)
    {
        var deleteResult = await _mediator.Send(new DeleteInvoiceCommand { Id = id,InvoiceType=invoiceType });
        return Ok(deleteResult);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var invoice = await _mediator.Send(new GetInvoiceByIdQuery { Id = id });
        return Ok(invoice);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize,InvoiceType invoiceType)
    {
        var invoices = await _mediator.Send(new GetAllInvoicesQuery(pageNumber, pageSize,x=>x.InvoiceType== invoiceType));
        return Ok(invoices);
    }


    


    //public SalesInvoiceControllers(IMapper mapper, AppDbContext context) : base(mapper, context)
    //{

    //}


    ////Satışlar modülü FiyaNotlar:
    ////Satış Kdv hariçde Birim Fiyat:SalesUnitPriceExcludingVAT
    ////Satış Kdv dahilde Birim Fiyat:SalesPriceIncludingVAT

    ////Tutar=Genel Toplam

    ////Satış eklendikçe satışın toplam tutarı müşterinin TotalBalance (Toplam Bakiyesine) yansıması lazım.
    ////Tahsil edildikçe bu da müşteriden   TotalBalance (Toplam Bakiyesine) düşülmesi gerekecektir.
    ////Ve tahsilatta ,satış tahsil edildikçe Satış faturası tablosundan kaln tutar düşülmelidir.

    //[HttpGet("[action]")]
    //public async Task<IActionResult> GetCreateTableData()
    //{
    //    var getDto = new List<GetSalesForCreateData>();

    //    var customers = await _context.Customer_Suppliers.Where(x => x.CustomerSupplierType == Customer_SupplierType.Customer)
    //        .Include(x => x.Contact).ToListAsync();

    //    customers.ForEach(async x =>
    //    {

    //        GetSalesForCreateData getSalesForCreateData = new GetSalesForCreateData();
    //        getSalesForCreateData.Id = x.Id;
    //        getSalesForCreateData.GeneralType = x.GeneralType;
    //        getSalesForCreateData.Address = x.Contact!.Address;
    //        getSalesForCreateData.City = x.Contact.City;
    //        getSalesForCreateData.Country = x.Contact.Country;
    //        getSalesForCreateData.District = x.Contact.District;
    //        getSalesForCreateData.TaxOffice = x.TaxOffice;
    //        getSalesForCreateData.OtherAddresses = x.OtherAddresses;

    //        if (x.GeneralType == GeneralType.Individual)
    //        {
    //            var individualCustomer = await _context.Customer_Suppliers.OfType<IndividualCustomer_Supplier>().SingleOrDefaultAsync(p => p.Id == x.Id);
    //            getSalesForCreateData.Title = $"{individualCustomer!.FirstName} {individualCustomer.LastName}";
    //        }
    //        getSalesForCreateData.Title = x.Title;
    //        getDto.Add(getSalesForCreateData);
    //    });

    //    List<GetSalesProductDto> getSalesProductDtos = new List<GetSalesProductDto>();

    //    var products = await _context.Products.ToListAsync();


    //    products.ForEach(x =>
    //    {
    //        getSalesProductDtos.Add(new GetSalesProductDto
    //        {
    //            ProductId = x.Id,
    //            Code = x.Code,
    //            ProductName = x.Name,
    //            SalesPriceIncludingVAT = x.SalesPriceIncludingVAT,
    //            TotalStockAmount = x.TotalStockAmount,
    //            UnitType = x.UnitType,
    //            SalesUnitPriceExcludingVAT = x.SalesUnitPriceExcludingVAT

    //        });
    //    });

    //    return Ok(new { Customers = customers, Products = getSalesProductDtos });
    //}


    ////Dikkat bütün hesaplamalar client mvc tarafta yapılmalı.Buraya salt data gelmeli
    //[HttpPost]
    //public async Task<IActionResult> Create(SalesInvoiceCreateDto createDto)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        if (createDto.ProductSalesInvoiceCreateDtos.Count == 0)
    //        {
    //            ModelState.AddModelError("", "En az bir ürün girilmelidir!");
    //            return BadRequest(ModelState.AddModelStateErrorResult());
    //        }

    //        SalesInvoice salesInvoice = new SalesInvoice()
    //        {
    //            CurrencyType = createDto.CurrencyType,
    //            CustomerId = createDto.Customer_SupplierId,
    //            Description = createDto.Description,
    //            Discount = createDto.Discount,
    //            DiscountType = createDto.DiscountType,
    //            DueDate = createDto.DueDate,
    //            ExchangeRate = createDto.ExchangeRate,
    //            InvoiceDate = createDto.InvoiceDate,
    //            InvoiceNumber = createDto.InvoiceNumber,
    //            IsAddressDifferent = createDto.IsAddressDifferent,
    //            OtherAddressId = createDto.OtherAddressId,
    //            SalesInvoiceCategoryId = createDto.SalesInvoiceCategoryId,
    //            SalesInvoiceType = createDto.SalesInvoiceType,
    //            ShipmentNumber = createDto.ShipmentNumber,
    //            ShipmentDate = createDto.ShipmentDate,
    //            Subtotal = createDto.Subtotal,
    //            TotalAmount = createDto.TotalAmount,
    //            RemainingAmount = createDto.TotalAmount,
    //            TotalDiscount = createDto.TotalDiscount,
    //            TotalVATAmount = createDto.TotalVATAmount,
    //        };

    //        var customer = await _context.Customer_Suppliers.FindAsync(createDto.Customer_SupplierId);
    //        customer!.TotalBalance += createDto.TotalAmount;


    //        if (customer!.GeneralType == GeneralType.Individual)
    //        {
    //            var indivualCustomer = await _context.IndividualCustomer_Suppliers.FindAsync(customer.Id);
    //            salesInvoice.FullNameOrUnvan = $"{indivualCustomer!.FirstName} {indivualCustomer.LastName}";
    //        }
    //        salesInvoice.FullNameOrUnvan = customer.Title;


    //        createDto.ProductSalesInvoiceCreateDtos.ToList().ForEach(x =>
    //        {
    //            salesInvoice.ProductSalesInvoices.Add(new ProductSalesInvoice
    //            {
    //                Discount = x.Discount,
    //                DiscountType = x.DiscountType,
    //                ProductQuantity = x.ProductQuantity,
    //                TaxAmount = x.TaxAmount,
    //                TaxRate = x.TaxRate,
    //                ProductdId = x.ProductdId,

    //                TotalSalesAmountForProduct = x.TotalSalesAmountForProduct,
    //                UnitPrice = x.UnitPrice,
    //                VATRate = x.VATRate,
    //                UnitType = x.UnitType,
    //            });

    //            var product = _context.Products.SingleOrDefault(t => t.Id == x.ProductdId);
    //            product!.TotalStockAmount -= x.ProductQuantity;
    //        });

    //        await _context.SalesInvoices.AddAsync(salesInvoice);
    //        await _context.SaveChangesAsync();
    //        return Ok();
    //    }
    //    return BadRequest(ModelState.AddModelStateErrorResult());
    //}



    //[HttpGet("[action]")]
    //public async Task<IActionResult> SalesInvoiceGetDataTable()
    //{
    //    var sales = await _context.SalesInvoices.ToListAsync();

    //    var salesDto = _mapper.Map<List<SalesInvoiceGetDataTableDto>>(sales);
    //    return Ok(salesDto);
    //}

    ////Satışlar modülü Fiyat Notlar:

    ////Satış Kdv hariçde => Birim Fiyat:SalesUnitPriceExcludingVAT 
    ////Satış Kdv dahilde =>  Birim Fiyat:SalesPriceIncludingVAT

    ////SalesInvoiceType'a göre yazılacak.

    ////Eğerki IsAddressDifferent true ise otherAdres alanıda dolu olacak.

    ////eğer teslimat adresi false ise contact'daki  adres 
    ////eğer true ise otherrAdress kısmındaki adresi getirsin.
    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetSalesInvoice(Guid id)
    //{
    //    var salesInvoice = await _context.SalesInvoices.Include(x => x.ProductSalesInvoices)
    //        .Include(x => x.Customer_Supplier).ThenInclude(t => t.OtherAddresses)
    //         .SingleOrDefaultAsync(x => x.Id == id);

    //    if (salesInvoice == null)
    //    {
    //        return NotFound(new ErrorResponseDto
    //        {
    //            PropertyName = string.Empty,
    //            ErrorMessage = "Satış faturası bulunamadı!"
    //        });
    //    }

    //    GetSalesInvoiceDto getSalesInvoiceDto = new GetSalesInvoiceDto()
    //    {
    //        Id = salesInvoice.Id,
    //        CurrencyType = salesInvoice.CurrencyType,
    //        Description = salesInvoice.Description,
    //        Discount = salesInvoice.Discount,
    //        DiscountType = salesInvoice.DiscountType,
    //        DueDate = salesInvoice.DueDate,
    //        ExchangeRate = salesInvoice.ExchangeRate,
    //        InvoiceDate = salesInvoice.InvoiceDate,
    //        InvoiceNumber = salesInvoice.InvoiceNumber,
    //        SalesInvoiceType = salesInvoice.SalesInvoiceType,
    //        SalesInvoiceCategoryId = salesInvoice.SalesInvoiceCategoryId,
    //        ShipmentDate = salesInvoice.ShipmentDate,
    //        ShipmentNumber = salesInvoice.ShipmentNumber,
    //        TotalVATAmount = salesInvoice.TotalVATAmount,
    //        TotalDiscount = salesInvoice.TotalDiscount,
    //        TotalAmount = salesInvoice.TotalAmount,
    //        Subtotal = salesInvoice.Subtotal,

    //        GeneralType = salesInvoice.Customer_Supplier!.GeneralType,
    //        Customer_SupplierId = salesInvoice.Customer_Supplier.Id,
    //    };

    //    if (salesInvoice!.IsAddressDifferent == true && salesInvoice.OtherAddressId != null)
    //    {
    //        var otherAddress = salesInvoice.Customer_Supplier.OtherAddresses!.Single(x => x.Id == salesInvoice.OtherAddressId);
    //        getSalesInvoiceDto.OtherAddress = otherAddress;
    //    }

    //    salesInvoice.ProductSalesInvoices.ToList().ForEach(x =>
    //    {
    //        getSalesInvoiceDto.ProductSalesInvoiceCreateDtos.Add(new ProductSalesInvoiceCreateDto
    //        {
    //            Discount = x.Discount,
    //            ProductdId = x.ProductdId,
    //            DiscountType = x.DiscountType,
    //            ProductQuantity = x.ProductQuantity,
    //            TaxAmount = x.TaxAmount,
    //            TaxRate = x.TaxRate,
    //            TotalSalesAmountForProduct = x.TotalSalesAmountForProduct,
    //            UnitPrice = x.UnitPrice,
    //            UnitType = x.UnitType,
    //            VATRate = x.VATRate
    //        });
    //    });
    //    return Ok(getSalesInvoiceDto);
    //}

    //[HttpGet]
    //public async Task<IActionResult> Update(GetSalesInvoiceDto updateDto)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        if (updateDto.ProductSalesInvoiceCreateDtos.Count == 0)
    //        {
    //            ModelState.AddModelError("", "En az bir ürün girilmelidir!");
    //            return BadRequest(ModelState.AddModelStateErrorResult());
    //        }

    //        SalesInvoice salesInvoice = new SalesInvoice()
    //        {
    //            CurrencyType = updateDto.CurrencyType,
    //            CustomerId = updateDto.Customer_SupplierId,
    //            Description = updateDto.Description,
    //            Discount = updateDto.Discount,
    //            DiscountType = updateDto.DiscountType,
    //            DueDate = updateDto.DueDate,
    //            ExchangeRate = updateDto.ExchangeRate,
    //            InvoiceDate = updateDto.InvoiceDate,
    //            InvoiceNumber = updateDto.InvoiceNumber,
    //            IsAddressDifferent = updateDto.IsAddressDifferent,
    //            OtherAddressId = updateDto.OtherAddressId,
    //            SalesInvoiceCategoryId = updateDto.SalesInvoiceCategoryId,
    //            SalesInvoiceType = updateDto.SalesInvoiceType,
    //            ShipmentNumber = updateDto.ShipmentNumber,
    //            ShipmentDate = updateDto.ShipmentDate,
    //            Subtotal = updateDto.Subtotal,
    //            TotalAmount = updateDto.TotalAmount,
    //            RemainingAmount = updateDto.TotalAmount,
    //            TotalDiscount = updateDto.TotalDiscount,
    //            TotalVATAmount = updateDto.TotalVATAmount,
    //        };


    //        var customer = await _context.Customer_Suppliers.FindAsync(updateDto.Customer_SupplierId);



    //        customer!.TotalBalance += updateDto.TotalAmount;


    //        return Ok();
    //    }
    //    return BadRequest(ModelState.AddModelStateErrorResult());
    //}
}
