using AspNetCoreHero.Abstractions.Domain;
using System;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;

/// <summary>
/// Satış faturlarında ürün eklemek için çoka çok tablosu
/// Base tablodan miras almadım.
/// satış tablosunda bununla ilgili BaseAuditableEntity değerleri tutulmaktadır. 
/// </summary>

public class ProductInvoice:AuditableEntity
{
    /// <summary>
    /// Ürün Miktarı
    /// </summary>
    public decimal ProductAmount { get; set; }

    /// <summary>
    /// Birim Fiyat
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Vergi Oranı Yüzde % KDV Oranı
    /// </summary>
    public decimal TaxRate { get; set; }

    /// <summary>
    /// Vergi Tutarı
    /// </summary>
    public decimal TaxAmount { get; set; }

    /// <summary>
    /// İndirim
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// İndirim Tipi
    /// </summary>
    public DiscountType DiscountType { get; set; }


    /// <summary>
    /// Toplam  Tutarı (Herhangi para birimi)
    /// Ürün Miktarı X  Birim Fiyat=Toplam Satış Tutarı
    /// </summary>
    public decimal TotalSalesAmountForProduct { get; set; }


      /// <summary>
    /// Son Kullanma Tarihi
    /// </summary>
    public string? ExpirationDate { get; set; }

    /// <summary>
    /// SerialOrLotNumber
    /// </summary>
    public string? SerialOrLotNumber { get; set; }
    public string? Description { get; set; }

    public int ProductServiceId { get; set; } 
    public ProductService ProductService { get; set; } 

    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }

    public int UnitId { get; set; }
    public Unit Unit { get; set; }

}

//public class ProductSalesInvoiceConfiguration : IEntityTypeConfiguration<ProductSalesInvoice>
//{
//    public void Configure(EntityTypeBuilder<ProductSalesInvoice> builder)
//    {
//        builder.HasKey(x => x.Id);

//        builder.HasOne(x => x.Product)
//             .WithMany(x => x.ProductSalesInvoices)
//             .HasForeignKey(x => x.ProductdId);

//        builder.HasOne(pc => pc.SalesInvoice)
//            .WithMany(c => c.ProductSalesInvoices)
//            .HasForeignKey(pc => pc.SalesInvoiceId);
//   }
//}
