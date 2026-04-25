using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Application.DTOs.Order;
using ZeusApp.Application.DTOs.OtherAddress;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Orders.Queries.GetById;
public class GetOrderByIdResponse
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
    public ICollection<GetByIdProductOrderResponse> ProductOrders { get; set; } = new HashSet<GetByIdProductOrderResponse>();
    public CustomerOtherAddressResponse CustomerOtherAddressResponse { get; set; }
}
