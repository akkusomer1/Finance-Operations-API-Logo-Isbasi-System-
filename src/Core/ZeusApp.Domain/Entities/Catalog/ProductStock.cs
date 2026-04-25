using System;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;

/// <summary>
/// Base tablodan miras almadım.
/// Stok tablosunda bununla ilgili AuditableEntity değerleri tutulmaktadır. 
/// </summary>
public class ProductStock : AuditableEntity
{
    public int ProductServiceId { get; set; }
    public int StockId { get; set; }

    public ProductService ProductService { get; set; }
    public Stock Stock { get; set; }

    /// <summary>
    ///   Birim
    /// </summary>

    public int UnitId { get; set; }
    public Unit Unit { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Hariç)
    /// </summary>
    public decimal PurchasePriceExcludingVAT { get; set; }


  
    /// <summary>
    /// Stok Miktarı
    /// </summary>
    public decimal StockAmount { get; set; } = 1;


    /// <summary>
    /// Toplam Tutar
    /// </summary>
    public decimal TotalAmount { get; set; }
}

//public class ProductStockConfiguration : IEntityTypeConfiguration<ProductStock>
//{
//    public void Configure(EntityTypeBuilder<ProductStock> builder)
//    {
//        builder.HasKey(x => x.Id);

//        builder.HasOne(x => x.Stock)
//             .WithMany(x => x.ProductStocks)
//             .HasForeignKey(x => x.StockId);

//        builder.HasOne(pc => pc.Product)
//            .WithMany(c => c.ProductStocks)
//            .HasForeignKey(pc => pc.ProductId);
//    }
//}
