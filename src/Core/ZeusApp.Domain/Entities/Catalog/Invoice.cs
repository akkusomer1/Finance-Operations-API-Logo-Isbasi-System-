using System;
using System.Collections.Generic;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;

/// <summary>
/// Satış faturaları tablosu
///Her satışın 1 tane müşterisi vardır 
///Ürün ve müşteri alanları çoka çok ilişki
///Dikkat!!: Satış ve alış faturaları hemen hemen aynı bu 2 tablo birleştirilebilir.
/// </summary>
public class Invoice : AuditableEntity
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
    public int? InvoiceCategoryId { get; set; }
    public InvoiceCategory? InvoiceCategory { get; set; }


    /// <summary>
    /// Ambar
    /// </summary>
    public int? HoldId { get; set; }
    public Hold?  Hold { get; set; }


    //Seri veya Lot numarası Seri veya lot numarası son kullanma tarihi

    /// <summary>
    /// Müşteri
    /// </summary>
    public int CustomerSupplierId { get; set; }
    public CustomerSupplier CustomerSupplier { get; set; }


    /// <summary>
    /// Eğer fatura siparişe aitse ilişki kurulacaktır.
    /// Eğerki fatura siparişe ait değilse OrderId nullable olacaktır.
    /// </summary>
    public int? OrderId { get; set; }
    public Order? Order { get; set; }

    /// <summary>
    /// Ürünler ve satış faturası arasında çoka çok ilişki ve tutulması gereken değerle burada tutuluyor.
    /// </summary>
    public ICollection<ProductInvoice> ProductInvoices { get; set; } = new HashSet<ProductInvoice>();
}

//public class SalesInvoiceConfiguration : IEntityTypeConfiguration<SalesInvoice>
//{
//    public void Configure(EntityTypeBuilder<SalesInvoice> builder)
//    {
//        builder
//          .HasOne(x => x.SalesInvoiceCategory)
//          .WithMany(x => x.SalesInvoices)
//          .HasForeignKey(x => x.SalesInvoiceCategoryId)
//          .OnDelete(DeleteBehavior.Restrict);
//    }
//}
