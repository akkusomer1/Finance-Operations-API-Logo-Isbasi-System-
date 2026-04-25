using AspNetCoreHero.Results;
using AutoMapper;
using MediatR;
using ZeusApp.Application.DTOs.Order;
using ZeusApp.Application.Interfaces.Repositories;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Orders.Commands.Update;
public class UpdateOrderCommand : IRequest<Result<UpdateOrderCommand>>
{
    public int Id { get; set; }

    /// <summary>
    /// Sipariş Tarihi
    /// </summary>
    public DateTime OrderDate { get; set; }


    /// <summary>
    ///Sipariş Numarası
    /// </summary>
    public string? OrderNumber { get; set; }

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
    /// Sipariş Kategorisi
    /// </summary>
    public int? OrderCategoryId { get; set; }


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
    /// Toplam :İndirimlerin düşürülmüş tutarı
    /// </summary>
    public decimal Total { get; set; }



    /// <summary>
    ///Sipariş Tipi-Toptan mı , Perakande mi?
    ///Toptansa Kvd hariç, Perakende ise kdv hariç
    ///Sadece Sipariş için gönderilecek.
    /// </summary>
    public OrderInvoiceType OrderInvoiceType { get; set; }


    /// <summary>
    ///Satış sipariş mi, alış faturası mi?
    /// </summary>
    public InvoiceType InvoiceType { get; set; }


    /// <summary>
    ///Kalan Tutar Tabloda göstereceğiz
    /// </summary>
    public decimal RemainingAmount { get; set; }


    /// <summary>
    /// Taşıyıcı Firma
    /// </summary>
    public int? CarrierCompanyId { get; set; }

    //Seri veya Lot numarası Seri veya lot numarası son kullanma tarihi
    /// <summary>
    /// Müşteri
    /// </summary>
    public int CustomerSupplierId { get; set; }

    /// <summary>
    /// Ödemeli mi?
    /// </summary>
    public bool HasPaid { get; set; }

    /// <summary>
    /// Müşteri Sipariş Numarası
    /// Sadece Satış siparişde var.
    /// </summary>
    public string? CustomerOrderNumber { get; set; }

    // <summary>
    /// Tedarikçi Sipariş Numarası
    /// Sadece Alış siparişde var.
    /// </summary>
    public string? SupplierOrderNumber { get; set; }


    /// <summary>
    /// Sipariş Durumu
    /// </summary>
    public OrderStatus OrderStatus { get; set; }


    /// <summary>
    /// Ürünler-Hizmet ve sipariş  arasında çoka çok ilişki ve tutulması gereken değerle burada tutuluyor.
    /// </summary>
    public ICollection<UpdateProductOrderRequest> ProductOrders { get; set; } = new HashSet<UpdateProductOrderRequest>();
}

//To Do: Daha önce ödenen miktarıda müşteri bakiyesinden düşürme işlemini kontrol et.

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result<UpdateOrderCommand>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductServiceRepository _productRepository;
    private readonly ICustomerSupplierRepository _customerSupplierRepository;
    private readonly IOrderRepository _OrderRepository;
    private readonly IProductOrderRepository _productOrderRepository;
    private readonly IMapper _mapper;
    public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IOrderRepository OrderRepository, IProductServiceRepository productRepository, ICustomerSupplierRepository customerSupplierRepository, IMapper mapper, IProductOrderRepository productOrderRepository)
    {
        _unitOfWork = unitOfWork;
        _OrderRepository = OrderRepository;
        _productRepository = productRepository;
        _customerSupplierRepository = customerSupplierRepository;
        _mapper = mapper;
        _productOrderRepository = productOrderRepository;
    }

    //Eğer sipariş faturalandıysa,siparişi güncelleyemez.Buna dikkat.
    public async Task<Result<UpdateOrderCommand>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _OrderRepository.GetByIdAsync(request.Id);

        if (order.OrderStatus != OrderStatus.waiting)
        {
            throw new Exception("Bu sipariş faturalandırılmıştır, güncellenemez!");
        }

        if (request.ProductOrders.Count == 0 || request.ProductOrders.Any(x => x.ProductServiceId == 0 || x.ProductServiceId == null))
        {
            throw new Exception("Lütfen Ürün/Hizmet seçiniz!");
        }

        if (request.CurrencyType != CurrencyType.TL && request.ExchangeRate is 0)
        {
            throw new Exception("Lütfen döviz kuru giriniz.!!");
        }

        //Eğerki isOtherAdress true ise 

        //Eğerki gelen değer satış ve perakende satışssa = Bitti
        if (request.InvoiceType == InvoiceType.selling)
        {
            //Siparişde ki eski ürünleri sil.

            foreach (var x in order.ProductOrders)
            {
                await _productOrderRepository.DeleteAsync(x);
            }

            if (request.OrderInvoiceType == OrderInvoiceType.PerakendeSiparisFaturasıKDVDahil)
            {
                //Toplam tutar hesaplama
                //Toplam indirimi tutar cinsinden al.
                decimal totalAmountDiscount = 0;
                request.ProductOrders.ToList().ForEach(p =>
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

                            var discountAmount = (p.TotalSalesAmountForProduct * p.Discount) / 100;
                            p.TotalSalesAmountForProduct = p.TotalSalesAmountForProduct - discountAmount;
                            totalAmountDiscount += discountAmount;
                        }
                    }
                    //ürüne ait vergi tutarını hesapla

                    decimal vatCalculate = (1 + (Convert.ToDecimal(p.TaxRate) / 100));

                    p.TaxAmount = p.TotalSalesAmountForProduct - (p.TotalSalesAmountForProduct / vatCalculate);
                });

                //request.TotalDiscount=request.ProductOrders.Sum(x=>x.)

                //Genel Toplam 
                request.TotalAmount = request.ProductOrders.Sum(x => x.TotalSalesAmountForProduct);

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
                request.Subtotal = request.ProductOrders.Sum(x => x.TotalSalesAmountForProduct);

                if (request.DiscountAmount != 0)
                {
                    foreach (var x in request.ProductOrders)
                    {
                        // Her bir ürüne düşecek indirim miktarı:
                        //Her ürüne ait toplam  fiyat/ Ara toplam * indirim fiyatı

                        decimal productDiscount = (x.TotalSalesAmountForProduct / request.Subtotal) * discountAmount;
                        x.TotalSalesAmountForProduct -= productDiscount;

                        //ürüne ait vergi tutarını tekrardan hesapla
                        decimal vatCalculate = (1 + (Convert.ToDecimal(x.TaxRate) / 100));

                        x.TaxAmount = x.TotalSalesAmountForProduct - (x.TotalSalesAmountForProduct / vatCalculate);

                        //Beklenen miktar en başta ProductAmount'ın eşittir.
                        x.PendingQuantity = x.ProductAmount;
                    }
                }
                //Güncel Genel toplamı hesapla 
                request.TotalAmount = request.ProductOrders.Sum(x => x.TotalSalesAmountForProduct);

                //indirimi toplam tutardan çıkar
                //request.TotalAmount -= discountAmount;

                //Toplam indirimi hesapla
                request.TotalDiscount = totalAmountDiscount + discountAmount;

                //Toplam Kdv Tutarı
                request.TotalVATAmount = request.ProductOrders.Sum(x => x.TaxAmount);

                //Toplam= (Genel Toplam - Toplam Kdv Tutarı)
                request.Total = request.TotalAmount - request.TotalVATAmount;
            }

            //Eğerki gelen değer satış ve toptan satışssa ilgili hesaplamaları yapar.

            if (request.OrderInvoiceType == OrderInvoiceType.ToptanSiparisFaturasiKDVHaric)
            {
                request = Calculate(request);

                //ilgili ürünlerin stoğunu güncelle.= true;
                foreach (var x in request.ProductOrders)
                {
                    x.PendingQuantity = x.ProductAmount;
                }
            }
            //Müşteriye borç eklenecek.=true;

            //Hem alışda hede satışda müşteri (Ödeyecek) veya tedarikçini (Tahsil Edilecek) Toplam bakiyesi arttar.

            //Eski miktarı ,döviz eğer tl değilse tl'ye çevir.

            //Bu esnada tahsil ettiği kısımlarıda hesaplayacağız.

            if (request.HasPaid)
            {
                if (order.CurrencyType != CurrencyType.TL)
                {
                    order.CustomerSupplier.TotalBalance -= order.ExchangeRate * order.TotalAmount;
                }
                else
                {
                    //Eskisini düşür.
                    order.CustomerSupplier.TotalBalance -= order.TotalAmount;
                }
                //Eski genel toplam çıktı.

                //Daha sonra müşterinin Toplamda ödediği miktarı düşür.      
                var amountPaid = order.TotalAmount - order.RemainingAmount;

                //Yeni miktarı ,döviz eğer tl değilse tl'ye çevir öyle ekle.
                //
                if (request.CurrencyType != CurrencyType.TL)
                {
                    order.CustomerSupplier.TotalBalance += (request.ExchangeRate * request.TotalAmount) - amountPaid;
                }
                else
                {
                    //Yeni miktarı ekle.
                    order.CustomerSupplier.TotalBalance += order.TotalAmount - amountPaid;
                }
                await _customerSupplierRepository.UpdateAsync(order.CustomerSupplier);
            }
        }

        //Eğerki alış faturası seçildiyse
        if (request.InvoiceType == InvoiceType.buying)
        {
            request = Calculate(request);


            //Eski miktarı ,döviz eğer tl değilse tl'ye çevir.

            if (request.HasPaid)
            {
                if (order.CurrencyType != CurrencyType.TL)
                {
                    order.CustomerSupplier.TotalBalance += order.ExchangeRate * order.TotalAmount;
                }
                else
                {
                    //Eski miktarı  arttır.
                    order.CustomerSupplier.TotalBalance += order.TotalAmount;
                }

                //Daha sonra müşterinin Toplamda ödediği miktarı düşür.      
                var amountPaid = order.TotalAmount - order.RemainingAmount;

                //Yeni miktarı ,döviz eğer tl değilse tl'ye çevir öyle ekle.
                if (request.CurrencyType != CurrencyType.TL)
                {
                    order.CustomerSupplier.TotalBalance -= (request.ExchangeRate * request.TotalAmount) - amountPaid;
                }
                else
                {
                    //Yeni miktarı düşür.
                    order.CustomerSupplier.TotalBalance -= order.TotalAmount - amountPaid;
                }

                await _customerSupplierRepository.UpdateAsync(order.CustomerSupplier);
            }
        }

        //Daha önce ödenen miktarı al.

        var amountPaids = order.TotalAmount - order.RemainingAmount;

        //Genel toplamdan ödenen miktarı çıkar bu da kalan borcumuzdur.
        order.RemainingAmount = request.TotalAmount - amountPaids;
     
        var orderMap = _mapper.Map<UpdateOrderCommand, Order>(request, order);
        await _OrderRepository.UpdateAsync(orderMap);

        await _unitOfWork.Commit(cancellationToken);
        return Result<UpdateOrderCommand>.Success(request, 200);

        //Eğerki alış faturası seçildiyse ilgili hesaplamaları yapar.
    }

    public UpdateOrderCommand Calculate(UpdateOrderCommand request)
    {
        decimal totalDiscount = 0;
        //Toplam tutar hesaplama- //Vergiler hariç
        request.ProductOrders.ToList().ForEach(p =>
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
        //var productAllAmount = request.ProductOrders.Sum(x => x.TotalSalesAmountForProduct);
        var taxAmount = request.ProductOrders.Sum(x => x.TaxAmount);

        //request.TotalAmount = productAllAmount + taxAmount;

        //Toplam= (Genel Toplam - Toplam Kdv Tutarı)
        request.Total = request.ProductOrders.Sum(x => x.TotalSalesAmountForProduct);
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
        request.Subtotal = request.ProductOrders.Sum(x => x.TotalSalesAmountForProduct) + totalDiscount;


        //Dikkat hatalı.
        if (request.DiscountAmount != 0)
        {
            request.ProductOrders.ToList().ForEach(x =>
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
                    notDiscountProductTotalPrice = x.TotalSalesAmountForProduct;
                }
                //  Her ürüne ait toplam  fiyat + ürüne özel indirim fiyatı / Ara toplam* indirim fiyatı
                decimal productDiscount = (notDiscountProductTotalPrice / request.Subtotal) * discountAmount;

                x.TotalSalesAmountForProduct -= productDiscount;

                //ürüne ait vergi tutarını tekrardan hesapla
                x.TaxAmount = x.TotalSalesAmountForProduct * (Convert.ToDecimal(x.TaxRate) / 100);
            });
        }

        //Toplam Kdv Tutarı
        request.TotalVATAmount = request.ProductOrders.Sum(x => x.TaxAmount);

        //Güncel Genel toplamı hesapla 
        request.TotalAmount = request.ProductOrders.Sum(x => x.TotalSalesAmountForProduct) + request.TotalVATAmount;

        //indirimi toplam tutardan çıkar
        //request.TotalAmount -= discountAmount;

        //Toplam indirimi hesapla
        request.TotalDiscount = totalDiscount + discountAmount;

        //Toplam= (Genel Toplam - Toplam Kdv Tutarı)
        request.Total = request.TotalAmount - request.TotalVATAmount;
        //    var Order = _mapper.Map<Order>(request);
        request.TotalAmount = request.Total + request.TotalVATAmount;

        return request;
    }
}