using AutoMapper;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.Features.Ayarlar.Commands.Create;
using ZeusApp.Application.Features.Stocks.Commands.Create;
using Microsoft.AspNetCore.Authorization;
using ZeusApp.Application.Features.Ayarlar.Commands.Update;
using ZeusApp.Application.Features.Stocks.Commands.Update;
using ZeusApp.Application.Features.Products.Commands.Delete;
using ZeusApp.Application.Features.Stocks.Commands.Delete;
using ZeusApp.Application.Features.Ayarlar.Queries.GetAllPaged;
using ZeusApp.Application.Features.StockCategories.Queries.GetAllPaged;
using ZeusApp.Application.Features.Stocks.Queries.GetAllPaged;
using ZeusApp.Application.Features.Ayarlar.Queries.GetById;
using ZeusApp.Application.Features.Stocks.Queries.GetById;
using ZeusApp.Application.Features.Stocks.Queries.GetDropdownList;

namespace ZeusApp.WebApi.Controllers.v2;

public class StockControllers : BaseApiController<StockControllers>
{


    [HttpPost("[action]")]
    public async Task<IActionResult> StockOutPost(CreateStockOutCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }



    [HttpPost("[action]")]
    public async Task<IActionResult> StockInPost(CreateStockInCommand command)
    {
        var createResult = await _mediator.Send(command);
        return Ok(createResult);
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateStockCommand command)
    { 
        var updateResult = await _mediator.Send(command);
        return Ok(updateResult);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteResult = await _mediator.Send(new DeleteStockCommand { Id = id });
        return Ok(deleteResult);
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
    {
        var ayarlar = await _mediator.Send(new GetAllStocksQuery(pageNumber, pageSize));
        return Ok(ayarlar);
    }

    //Dropdown içerisinde ürünler ve ilgili bilgilerini doldurmak için...
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllStockProductsForDropDown()
    {
        var stockProducts = await _mediator.Send(new GetAllStockProductsQuery());
        return Ok(stockProducts);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var stock = await _mediator.Send(new GetStockByIdQuery { Id = id });
        return Ok(stock);
    }

    //    //Stock giriş  formunda ürünler kısmındaki, ürünler dropdown ve Birim alanlarını doldurmak için
    //    [HttpGet("[action]")]
    //    public async Task<IActionResult> StockInCreateProductData()
    //    {
    //        var product = await _context.Products.Include(x => x.ProductStocks).OrderByDescending(x => x.CreatedDate).Select(x => new StockProductDto
    //        {
    //            ProductId = x.Id,
    //            ProductName = x.Name,
    //            UnitType = x.UnitType,
    //            PurchasePriceExcludingVAT = x.PurchasePriceExcludingVAT,
    //            TotalStockAmount = x.TotalStockAmount
    //        }).ToListAsync();

    //        return Ok(product);
    //    }


    //    //Stock Çıkış  formunda ürünler kısmındaki, ürünler dropdown ve Birim alanlarını doldurmak için


    //    [HttpGet("[action]")]
    //    public async Task<IActionResult> StockOutCreateProductData()
    //    {
    //        var product = await _context.Products.Include(x => x.ProductStocks).OrderByDescending(x => x.CreatedDate).Select(x => new
    //        {
    //            ProductId = x.Id,
    //            TotalStockAmount = x.TotalStockAmount,
    //            ProductName = x.Name,
    //            UnitType = x.UnitType,
    //        }).ToListAsync();
    //        return Ok(product);
    //    }

}
