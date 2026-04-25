using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Application.DTOs.Invoice;
using ZeusApp.Application.Enums;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Invoices.Commands.Create;
public class CreateInvoiceCommand : IRequest<Result<Invoice>>
{
    /// <summary>
    /// Fatura Tarihi
    /// </summary>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    /// Sevk Tarihi
    /// </summary>
    public DateTime ShipmentDate { get; set; }

    /// <summary>
    /// Vade Tarihi
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    ///Fatura Numarası
    /// </summary>
    public string? InvoiceNumber { get; set; }


    /// <summary>
    /// Döviz
    /// </summary>
    public CurrencyType CurrencyType { get; set; }

    /// <summary>
    /// Döviz Kuru
    /// </summary>
    public decimal ExchangeRate { get; set; }

    /// <summary>
    /// Açıklama
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ara Toplam
    /// </summary>
    public decimal Subtotal { get; set; }

    /// <summary>
    /// İndirim Tipi
    /// </summary>
    public DiscountType DiscountType { get; set; }


    /// <summary>
    /// İndirim tutarı
    /// </summary>
    public decimal DiscountAmount { get; set; }


    /// <summary>
    /// Toplam İndirim
    /// </summary>
    public decimal TotalDiscount { get; set; }

    /// <summary>
    /// Genel Toplam
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Toplam KDV Tutarı
    /// </summary>
    public decimal TotalVATAmount { get; set; }



    /// <summary>
    /// İndirimlerin düşürülmüş tutarı
    /// </summary>
    public decimal Total { get; set; }


    /// <summary>
    ///Satış Tipi-Toptan mı , Perakande mi?
    ///Toptansa Kvd hariç, Perakende ise kdv hariç
    ///Sadece Satış için gönderilecek.
    /// </summary>
    public SalesInvoiceType SalesInvoiceType { get; set; }


    /// <summary>
    ///Satış faturası mı, alış faturası mı?
    /// </summary>
    public InvoiceType InvoiceType { get; set; }


    /// <summary>
    /// Teslimat adresi farklı mı?
    /// </summary>
    public bool IsAddressDifferent { get; set; }


    /// <summary>
    /// Teslimat adresi (Eğer farklıysa)
    /// </summary>
    public int? OtherAddressId { get; set; }


    /// <summary>
    ///Kalan Tutar Tabloda göstereceğiz
    /// </summary>
    public decimal RemainingAmount { get; set; }


    /// <summary>
    /// Sevkiyat No 
    /// Sadece satış faturasında girilecektir.
    /// </summary>
    public string? ShipmentNumber { get; set; }


    /// <summary>
    /// Satış Fatura kategorisi
    /// </summary>
    /// 
    public int? InvoiceCategoryId { get; set; }

    /// <summary>
    /// Müşteri
    /// </summary>
    /// 
    [Required(ErrorMessage = "Müşteri alanı zorunludur.")]
    public int CustomerSupplierId { get; set; }


    /// <summary>
    /// Ambar
    /// </summary>
    public int? HoldId { get; set; }


    /// <summary>
    /// Ürünler ve satış faturası arasında çoka çok ilişki ve tutulması gereken değerle burada tutuluyor.
    /// </summary>
    public ICollection<ProductInvoiceRequest> ProductInvoices { get; set; } = new HashSet<ProductInvoiceRequest>();
}


public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<Invoice>>
{

    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IProductServiceRepository _productRepository;
    private readonly ICustomerSupplierRepository _customerSupplierRepository;
    private readonly IMapper _mapper;


    public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IMapper mapper, IUnitOfWork unitOfWork, IProductServiceRepository productRepository, ICustomerSupplierRepository customerSupplierRepository)
    {
        _invoiceRepository = invoiceRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _customerSupplierRepository = customerSupplierRepository;
    }

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    private IUnitOfWork _unitOfWork { get; set; }


    //Müşteriye borç eklenecek.=true;
    //ilgili ürünlerin stoğunu güncelle.= true;

    //Daha sonra tahsilat ve ödeme ile bağlantısını sağla.=false;
    //Hizmetlerle nasıl reaksiyon verdiğini incele=false;

    //Alış faturası tedarikçiler gelecek ve tedarikçiye öde (ödenecek).
    //Satış faturası müşteriler gelecek ve müşterilerden tahsil et (tahsil edilecek).

    public async Task<Result<Invoice>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        if (request.ProductInvoices.Count == 0 || request.ProductInvoices.Any(x => x.ProductServiceId == 0 || x.ProductServiceId == null))
        {
            throw new Exception("Lütfen Ürün/Hizmet seçiniz!");
        }

        if (request.CurrencyType != CurrencyType.TL && request.ExchangeRate is 0)
        {
            throw new Exception("Lütfen döviz kuru giriniz!");
        }

        //Eğerki isOtherAdress true ise 

        //Eğerki gelen değer satış ve perakende satışssa = Bitti
        if (request.InvoiceType == InvoiceType.selling)
        {
            if (request.SalesInvoiceType == SalesInvoiceType.PerakendeSatisFaturasıKDVDahil)
            {
                //Toplam tutar hesaplama
                //Toplam indirimi tutar cinsinden al.

                decimal totalAmountDiscount = 0;
                foreach (var p in request.ProductInvoices)
                {
                    p.TotalSalesAmountForProduct = p.ProductAmount * p.UnitPrice;

                    //İndirimi hesapla ve  toplama tutardan çıkar.
                    if (p.Discount != 0)
                    {
                        if (p.DiscountType == DiscountType.Amount)
                        {
                            p.TotalSalesAmountForProduct -= p.Discount;
                            totalAmountDiscount += p.Discount;
                        }
                        else
                        {
                            p.Discount = p.Discount > 100 ? 100 : p.Discount;

                            var discountAmountt = (p.TotalSalesAmountForProduct * p.Discount) / 100;
                            p.TotalSalesAmountForProduct = p.TotalSalesAmountForProduct - discountAmountt;
                            totalAmountDiscount += discountAmountt;
                        }
                    }
                    //ürüne ait vergi tutarını hesapla

                    decimal vatCalculate = (1 + (Convert.ToDecimal(p.TaxRate) / 100));

                    p.TaxAmount = p.TotalSalesAmountForProduct - (p.TotalSalesAmountForProduct / vatCalculate);
                }


                //request.TotalDiscount=request.ProductInvoices.Sum(x=>x.)


                //Genel Toplam 
                request.TotalAmount = request.ProductInvoices.Sum(x => x.TotalSalesAmountForProduct);

                decimal discountAmount = 0;
                if (request.DiscountAmount != 0 && request.DiscountType == DiscountType.Ratio)
                {
                    request.DiscountAmount = request.DiscountAmount > 100 ? 100 : request.DiscountAmount;

                    discountAmount = request.TotalAmount * (request.DiscountAmount / 100);
                }
                else if (request.DiscountAmount != 0 && request.DiscountType == DiscountType.Amount)
                {
                    discountAmount = request.DiscountAmount;
                }


                //Ara Toplam= Dikkat!! Faturadaki genel toplam indirim ara toplamdan düşürülmez.
                request.Subtotal = request.ProductInvoices.Sum(x => x.TotalSalesAmountForProduct);

                if (request.DiscountAmount != 0)
                {
                    foreach (var x in request.ProductInvoices)
                    {
                        // Her bir ürüne düşecek indirim miktarı:
                        //Her ürüne ait toplam  fiyat/ Ara toplam * indirim fiyatı

                        decimal productDiscount = (x.TotalSalesAmountForProduct / request.Subtotal) * discountAmount;
                        x.TotalSalesAmountForProduct -= productDiscount;

                        //ürüne ait vergi tutarını tekrardan hesapla
                        decimal vatCalculate = (1 + (Convert.ToDecimal(x.TaxRate) / 100));

                        x.TaxAmount = x.TotalSalesAmountForProduct - (x.TotalSalesAmountForProduct / vatCalculate);
                    }
                }
                //Güncel Genel toplamı hesapla 
                request.TotalAmount = request.ProductInvoices.Sum(x => x.TotalSalesAmountForProduct);

                //indirimi toplam tutardan çıkar
                //request.TotalAmount -= discountAmount;

                //Toplam indirimi hesapla
                request.TotalDiscount = totalAmountDiscount + discountAmount;

                //Toplam Kdv Tutarı
                request.TotalVATAmount = request.ProductInvoices.Sum(x => x.TaxAmount);

                //Toplam= (Genel Toplam - Toplam Kdv Tutarı)
                request.Total = request.TotalAmount - request.TotalVATAmount;

                //ilgili ürünlerin stoğunu güncelle.= true;

                foreach (var x in request.ProductInvoices)
                {
                    var product = _productRepository.ProductServices.SingleOrDefault(p => p.Id == x.ProductServiceId);
                    if (product!.ProductServiceType == ProductServiceType.product)
                    {
                        product!.TotalStockAmount -= x.ProductAmount;
                        await _productRepository.UpdateAsync(product);
                    }
                }
                request.RemainingAmount = request.TotalAmount;
            }


            //Eğerki gelen değer satış ve toptan satışssa
            if (request.SalesInvoiceType == SalesInvoiceType.ToptanSatisFaturasiKDVHaric)
            {
                request = Calculate(request);

                //ilgili ürünlerin stoğunu güncelle.= true;

                foreach (var x in request.ProductInvoices)
                {
                    var product = _productRepository.ProductServices.SingleOrDefault(p => p.Id == x.ProductServiceId);
                    if (product!.ProductServiceType == ProductServiceType.product)
                    {
                        product!.TotalStockAmount -= x.ProductAmount;
                        await _productRepository.UpdateAsync(product);
                    }
                }
                request.RemainingAmount = request.TotalAmount;
            }


            //Müşteriye borç eklenecek.=true;
            //Eğer tahsil ettiği kısım varsa buda hesaplanacak.
            var customer = await _customerSupplierRepository.CustomerSuppliers.SingleOrDefaultAsync(x => x.Id == request.CustomerSupplierId);

            //Döviz eğer tl değilse tl'ye çevir.
            decimal exchangeTotal = request.TotalAmount;

            if (request.CurrencyType != CurrencyType.TL)
            {
                exchangeTotal = request.ExchangeRate * request.TotalAmount;
            }

            customer!.TotalBalance += exchangeTotal;

            await _customerSupplierRepository.UpdateAsync(customer);
        }

        //Eğerki alış faturası seçildiyse
        if (request.InvoiceType == InvoiceType.buying)
        {
            request = Calculate(request);

            //ilgili ürünlerin stoğunu güncelle.= true;

            foreach (var x in request.ProductInvoices)
            {
                var product = _productRepository.ProductServices.SingleOrDefault(p => p.Id == x.ProductServiceId);
                if (product!.ProductServiceType == ProductServiceType.product)
                {
                    product!.TotalStockAmount += x.ProductAmount;
                    await _productRepository.UpdateAsync(product);
                }
            }
         
            //Tedarikçi veya Müşteri/Tedarikçiyr toplam bakiyesi düşürülür.
            var supplier = await _customerSupplierRepository.CustomerSuppliers.SingleOrDefaultAsync(x => x.Id == request.CustomerSupplierId);

            //Döviz eğer tl değilse tl'ye çevir.
            decimal exchangeTotal = request.TotalAmount;

            if (request.CurrencyType != CurrencyType.TL)
            {
                exchangeTotal = request.ExchangeRate * request.TotalAmount;
            }

            supplier!.TotalBalance -= exchangeTotal;

            await _customerSupplierRepository.UpdateAsync(supplier);

            request.RemainingAmount = request.TotalAmount;
        }

     

        var invoice = _mapper.Map<Invoice>(request);

        await _invoiceRepository.InsertAsync(invoice);

        await _unitOfWork.Commit(cancellationToken);
        return Result<Invoice>.Success(invoice, 200);
    }


    //Eğerki gelen değer satış ve toptan satışssa ilgili hesaplamaları yapar.
    //Eğerki alış faturası seçildiyse ilgili hesaplamaları yapar.
    public CreateInvoiceCommand Calculate(CreateInvoiceCommand request)
    {
        decimal totalDiscount = 0;
        //Toplam tutar hesaplama- //Vergiler hariç
        request.ProductInvoices.ToList().ForEach(p =>
        {
            p.TotalSalesAmountForProduct = p.ProductAmount * p.UnitPrice;

            //İndirimi hesapla ve  toplama tutardan çıkar.

            if (p.Discount != 0)
            {
                if (p.DiscountType == DiscountType.Amount)
                {
                    p.TotalSalesAmountForProduct -= p.Discount;
                    totalDiscount += p.Discount;
                }
                else
                {
                    p.Discount = p.Discount > 100 ? 100 : p.Discount;

                    var discountAmount = (p.TotalSalesAmountForProduct * p.Discount) / 100;
                    p.TotalSalesAmountForProduct = p.TotalSalesAmountForProduct - discountAmount;
                    totalDiscount += discountAmount;
                }
            }

            //ürüne ait vergi tutarını hesapla                   
            p.TaxAmount = p.TotalSalesAmountForProduct * (Convert.ToDecimal(p.TaxRate) / 100);
        });


        //Genel Toplam =Tüm productların (TotalSalesAmountForProduct + vetgi tutarı)
        //var productAllAmount = request.ProductInvoices.Sum(x => x.TotalSalesAmountForProduct);
        var taxAmount = request.ProductInvoices.Sum(x => x.TaxAmount);

        //request.TotalAmount = productAllAmount + taxAmount;

        //Toplam= (Genel Toplam - Toplam Kdv Tutarı)
        request.Total = request.ProductInvoices.Sum(x => x.TotalSalesAmountForProduct);
        //x
        decimal discountAmount = 0;
        if (request.DiscountAmount != 0 && request.DiscountType == DiscountType.Ratio)
        {
            request.DiscountAmount = request.DiscountAmount > 100 ? 100 : request.DiscountAmount;

            discountAmount = request.Total * (request.DiscountAmount / 100);
        }
        else if (request.DiscountAmount != 0 && request.DiscountType == DiscountType.Amount)
        {
            discountAmount = request.DiscountAmount;
        }

        //Ara Toplam= Dikkat!! Faturadaki genel toplam indirim ara toplamdan düşürülmez.
        request.Subtotal = request.ProductInvoices.Sum(x => x.TotalSalesAmountForProduct) + totalDiscount;


        if (request.DiscountAmount != 0)
        {
            foreach (var x in request.ProductInvoices)
            {
                // Her bir ürüne düşecek indirim miktarı:
                //Her ürüne ait toplam  fiyat + ürüne özel indirim fiyatı / Ara toplam * indirim fiyatı

                decimal notDiscountProductTotalPrice = 0;
                if (x.Discount != 0)
                {
                    if (x.DiscountType == DiscountType.Amount)
                    {
                        // İndirimsiz fiyat hesapla
                        notDiscountProductTotalPrice = x.TotalSalesAmountForProduct + x.Discount;
                    }
                    else
                    {
                        x.Discount = x.Discount > 100 ? 100 : x.Discount;

                        //İndirimsiz fiyat hesapla
                        notDiscountProductTotalPrice = x.TotalSalesAmountForProduct / (1 - (x.Discount / 100));
                    }
                }

                else
                {
                    //Eğer ürünlere indirim girilmemişse 0 değeri alamamak için.
                    notDiscountProductTotalPrice = x.TotalSalesAmountForProduct;
                }
                //  Her ürüne ait toplam  fiyat + ürüne özel indirim fiyatı / Ara toplam* indirim fiyatı
                decimal productDiscount = (notDiscountProductTotalPrice / request.Subtotal) * discountAmount;

                x.TotalSalesAmountForProduct -= productDiscount;

                //ürüne ait vergi tutarını tekrardan hesapla
                x.TaxAmount = x.TotalSalesAmountForProduct * (Convert.ToDecimal(x.TaxRate) / 100);
            }
        }

        //Toplam Kdv Tutarı
        request.TotalVATAmount = request.ProductInvoices.Sum(x => x.TaxAmount);

        //Güncel Genel toplamı hesapla 
        request.TotalAmount = request.ProductInvoices.Sum(x => x.TotalSalesAmountForProduct) + request.TotalVATAmount;

        //indirimi toplam tutardan çıkar
        //request.TotalAmount -= discountAmount;

        //Toplam indirimi hesapla
        request.TotalDiscount = totalDiscount + discountAmount;

        //Toplam= (Genel Toplam - Toplam Kdv Tutarı)
        request.Total = request.TotalAmount - request.TotalVATAmount;
        //    var invoice = _mapper.Map<Invoice>(request);
        request.TotalAmount = request.Total + request.TotalVATAmount;

        return request;
    }
}


