using System;
using System.Collections.Generic;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;

public class ProductService : AuditableEntity
{
    /// <summary>
    /// Kodu
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Ad
    /// </summary>
    public string? Name { get; set; } 


    /// <summary>
    /// KDV Oranı (%)
    /// </summary>
    public int VATRate { get; set; }

    /// <summary>
    /// Toplam Stok Miktarı=Gerçek Stok Miktarı
    /// Dikkat stok eklerken,stok çıkarken,satış faturası ve alış faturası kesilince bu TotalStockAmount miktarı değişir.
    /// </summary>
    public decimal TotalStockAmount { get; set; }


    /// <summary>
    /// Para Birimi
    /// </summary>
    public CurrencyType CurrencyType { get; set; }

    /// <summary>
    /// SATIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal SalesPriceIncludingVAT { get; set; }

    /// <summary>
    /// SATIŞ Birim Fiyat (KDV Hariç)
    /// </summary>
    public decimal SalesUnitPriceExcludingVAT { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal PurchasePriceIncludingVAT { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Hariç)
    /// </summary>
    public decimal PurchasePriceExcludingVAT { get; set; }


    /// <summary>
    /// Barkod
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Ürün Adı (2)
    /// </summary>
    public string? ProductName2 { get; set; }

    /// <summary>
    /// İzleme Yöntemi
    /// </summary>
    public TrackingType TrackingType { get; set; }
    //Navigation

    /// <summary>
    /// Ürün kategori
    /// </summary>
    public int? ProductServiceCategoryId { get; set; }
    public ProductServiceCategory ProductServiceCategory { get; set; }

    /// <summary>
    /// Sadece hizmete özel
    /// </summary>
    public int? ServiceGroupId { get; set; }
    public ServiceGroup ServiceGroup { get; set; }
    /// <summary>
    /// Marka
    /// </summary>
    public int? ProductBrandId { get; set; }
    public ProductBrand ProductBrand { get; set; }

    /// <summary>
    /// Hizmet mi , ürün mü?
    /// </summary>

    public ProductServiceType ProductServiceType { get; set; }

    /// <summary>
    /// Birimi 
    /// </summary>
    /// 
    public int? UnitId { get; set; }
    public Unit Unit { get; set; }

    public ICollection<ProductStock> ProductStocks { get; set; } = new HashSet<ProductStock>();
    public ICollection<ProductInvoice>? ProductInvoices { get; set; } = new HashSet<ProductInvoice>();
    public ICollection<ProductOrder> ProductOrders { get; set; } = new HashSet<ProductOrder>();

    public ICollection<ExpenseService> ExpenseServices { get; set; } = new HashSet<ExpenseService>();
}


//public class ProductConfiguration : IEntityTypeConfiguration<Product>
//{
//    public void Configure(EntityTypeBuilder<Product> builder)
//    {
//        builder
//            .HasOne(x=>x.ProductCategory)
//            .WithMany(x=>x.Products)
//            .HasForeignKey(x=>x.ProductCategoryId)
//            .OnDelete(DeleteBehavior.Restrict);

//        builder
//           .HasOne(x => x.ProductBrand)
//           .WithMany(x => x.Products)
//           .HasForeignKey(x => x.ProductBrandId)
//           .OnDelete(DeleteBehavior.Restrict);
//    }
//}
