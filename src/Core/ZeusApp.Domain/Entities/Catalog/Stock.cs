using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;

/// <summary>
/// Birim Fiyat= Product'ın ALIŞ FİYATI -> Birim Fiyat (KDV Hariç)
/// Toplam Tutar (TotalAmount)= Miktar x  Birim Fiyat |  Amount * PurchasePriceExcludingVAT;
/// Genel Toplam=Bu stokda seçilen tüm Productların TotalAmount'u olacak
/// </summary>

public class Stock : AuditableEntity
{
    /// <summary>
    /// Tarih kullanıcı kendisi herhangi bir tarih girmek istiyor.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Belge No 
    /// </summary>
    public string DocumentNo { get; set; }

    /// <summary>
    ///Kategori
    /// </summary>
    public int? StockCategoryId { get; set; }

    public StockCategory StockCategory { get; set; }

    /// <summary>
    ///Stok Tipi Giriş yada Çıkış
    /// </summary>
    public StockType StockType { get; set; }


    /// <summary>
    /// Açıklama
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Genel Stok  Harektet Türü
    /// </summary>
    public MovementType MovementType { get; set; }

    //Stock girişe ait bilgiler

    /// <summary>
    /// Döviz
    /// </summary>
    public CurrencyType? Currency { get; set; }

    /// <summary>
    /// Döviz Kuru
    /// </summary>
    public decimal ExchangeRate { get; set; }

    /// <summary>
    /// Genel Toplam Tl veya Herhangi Döviz
    /// </summary>
    public decimal GrandTotal { get; set; }

   
    /// <summary>
    ///  Stoğa ürün eklerken çoka çok ilişki.
    ///  Dikkat! aynı ürünü aynı stoğa bir daha ekleyebilir.
    /// </summary>
    public ICollection<ProductStock> ProductStocks { get; set; } = new HashSet<ProductStock>();
}

//public class StockConfiguration : IEntityTypeConfiguration<Stock>
//{
//    public void Configure(EntityTypeBuilder<Stock> builder)
//    {
//        builder.HasOne(x => x.StockCategory)
//            .WithMany(x => x.Stocks)
//            .HasForeignKey(x => x.StockCategoryId)
//            .OnDelete(DeleteBehavior.Restrict);
//    }
//}


