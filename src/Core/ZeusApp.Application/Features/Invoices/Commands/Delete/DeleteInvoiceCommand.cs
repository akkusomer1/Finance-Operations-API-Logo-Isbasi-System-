using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.Features.CustomerCategories.Commands.Delete;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Invoices.Commands.Delete;
public class DeleteInvoiceCommand : IRequest<Result<int>>
{
    public int Id { get; set; }


    /// <summary>
    ///Satış faturası mı, alış faturası mı?
    /// </summary>
    public InvoiceType InvoiceType { get; set; }
}

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, Result<int>>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductServiceRepository _productRepository;
    private readonly ICustomerSupplierRepository _customerSupplierRepository;

    public DeleteInvoiceCommandHandler(IInvoiceRepository invoiceRepository,
        IUnitOfWork unitOfWork, IMapper mapper, IProductServiceRepository productRepository, ICustomerSupplierRepository customerSupplierRepository)
    {
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _customerSupplierRepository = customerSupplierRepository;
    }



    //Eğerki  fatura iptal edilirse stok miktarını ve müsteri/tedarikçinin toplam bakiyesini güncelle.Bu durumda satış faturalarında stok miktarı artar.
    //Eğerki fatura silinirse edilirse stok miktarını ve müsteri/tedarikçinin toplam bakiyesini güncelle.Bu durumda satış faturalarında stok miktarı artar.
    //Alış  faturalarında ise stok düşer.


    //Dikkat eğer faturanın bir tahsilatı varsa silmesini engelliyor.
    //Faturayı silmek için tahsilatıbsilmeni gerekiyor mesajı veriyoruz.

    public async Task<Result<int>> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(request.Id);


        if (invoice == null)
        {
            throw new KeyNotFoundException("Böyle bir fatura bulunamadı.");
        }

        if (true)
        {
            //Dikkat Eğerki tahsilatı varsa silmesini engelliyoruz.
        }

        //Eğerki gelen değer satış ve perakende satışssa = Bitti
        if (request.InvoiceType == InvoiceType.selling)
        {
            //Bu faturaya  ait ürünlerdeki stok miktarını arttır.

            foreach (var x in invoice.ProductInvoices)
            {
                x.ProductService.TotalStockAmount += x.ProductAmount;
                await _productRepository.UpdateAsync(x.ProductService);
            }

            //Toplam tutarı Tl olarak düşürmek gerekir.
            //Kalan tutarı Tl olarak düşürmek gerekir.
            if (invoice.CurrencyType != CurrencyType.TL)
            {
                invoice.TotalAmount = invoice.ExchangeRate * invoice.TotalAmount;
            }
            invoice.CustomerSupplier.TotalBalance -= invoice.TotalAmount;
        }
        //Eğerki alış faturası seçildiyse
        if (request.InvoiceType == InvoiceType.buying)
        {

            foreach (var x in invoice.ProductInvoices)
            {
                x.ProductService.TotalStockAmount -= x.ProductAmount;
                await _productRepository.UpdateAsync(x.ProductService);
            }

            //Toplam tutarı Tl olarak arttırmak gerekir.
            if (invoice.CurrencyType != CurrencyType.TL)
            {
                invoice.TotalAmount = invoice.ExchangeRate * invoice.TotalAmount;
            }
            invoice.CustomerSupplier.TotalBalance += invoice.TotalAmount;
            await _customerSupplierRepository.UpdateAsync(invoice.CustomerSupplier);
        }

        await _invoiceRepository.DeleteAsync(invoice);
        return Result<int>.Success(invoice.Id, 200);
    }
}