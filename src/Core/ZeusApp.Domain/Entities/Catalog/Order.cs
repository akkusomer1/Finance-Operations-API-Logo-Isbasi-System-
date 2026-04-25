using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;
public class Order : AuditableEntity
{

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
    public OrderCategory? OrderCategory { get; set; }





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
    public CarrierCompany? CarrierCompany { get; set; }

    //Seri veya Lot numarası Seri veya lot numarası son kullanma tarihi
    /// <summary>
    /// Müşteri
    /// </summary>
    public int CustomerSupplierId { get; set; }
    public CustomerSupplier CustomerSupplier { get; set; }

    /// <summary>
    /// Ödemeli mi?
    /// </summary>
    public bool HasPaid { get; set; }


    /// <summary>
    /// Sipariş Durumu
    /// </summary>
    public OrderStatus OrderStatus { get; set; }
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
    /// Teslimat adresi farklı mı?
    /// </summary>
    public bool IsAddressDifferent { get; set; }


    /// <summary>
    /// Teslimat adresi (Eğer farklıysa)
    /// </summary>
    public int? OtherAddressId { get; set; }

    /// <summary>
    /// Ürünler-Hizmet ve sipariş  arasında çoka çok ilişki ve tutulması gereken değerle burada tutuluyor.
    /// </summary>
    public ICollection<ProductOrder> ProductOrders { get; set; } = new HashSet<ProductOrder>();
    public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();


}
