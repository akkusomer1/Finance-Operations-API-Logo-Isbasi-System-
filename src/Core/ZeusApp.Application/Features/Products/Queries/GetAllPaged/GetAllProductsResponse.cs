using System.ComponentModel.DataAnnotations;

namespace ZeusApp.Application.Features.Products.Queries.GetAllPaged;
public class GetAllProductsResponse
{
    public int Id { get; set; }

    [Display(Name = "Ürün Kodu")]
    public string Code { get; set; } = null!;


    [Display(Name = "Ad")]
    public string Name { get; set; } = null!;


    [Display(Name = "Birimi")]
    public string UnitName { get; set; }


    [Display(Name = "KDV Oranı (%)")]
    public int VATRate { get; set; }


    [Display(Name = "ALIŞ Birim Fiyat (KDV Dahil)")]
    public decimal PurchasePriceIncludingVAT { get; set; }


    [Display(Name = "SATIŞ Birim Fiyat (KDV Dahil)")]
    public decimal SalesPriceIncludingVAT { get; set; }


    [Display(Name = "Toplam Stok Miktarı")]
    public decimal TotalStockAmount { get; set; }


}

