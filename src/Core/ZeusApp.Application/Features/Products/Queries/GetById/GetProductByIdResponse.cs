using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Products.Queries.GetById;
public class GetProductByIdResponse
{
    public int Id { get; set; }

    public int Status { get; set; }

    [Display(Name = "Kodu")]
    [Required(ErrorMessage = "{0} alanın doldurulması zorunludur.")]
    public string Code { get; set; } = null!;


    [Display(Name = "Ad")]
    [Required(ErrorMessage = "{0} alanın doldurulması zorunludur.")]
    public string Name { get; set; } = null!;


    [Display(Name = "KDV Oranı (%)")]
    public int VATRate { get; set; }


    [Display(Name = "Para Birimi")]
    public CurrencyType CurrencyType { get; set; }

    /// <summary>
    /// İzleme Yöntemi lot veya seri numarası
    /// </summary>
    public TrackingType TrackingType { get; set; }

    [Display(Name = "SATIŞ Birim Fiyat (KDV Dahil)")]
    public decimal SalesPriceIncludingVAT { get; set; }


    [Display(Name = "Birim Fiyat (KDV Hariç)")]
    public decimal UnitPriceExcludingVAT { get; set; }


    [Display(Name = "ALIŞ Birim Fiyat (KDV Dahil)")]
    public decimal PurchasePriceIncludingVAT { get; set; }


    [Display(Name = "ALIŞ Birim Fiyat (KDV Hariç)")]
    public decimal PurchasePriceExcludingVAT { get; set; }


    [Display(Name = "Barkod")]
    public string? Barcode { get; set; }

    [Display(Name = "Ürün Adı (2)")]
    public string? ProductName2 { get; set; }

    [Display(Name = "Kategori")]
    public Guid? ProductCategoryId { get; set; }


    [Display(Name = "Marka")]
    public Guid? ProductBrandId { get; set; }


    [Display(Name = "Birimi")]
    // [Required(ErrorMessage = "{0} alanın doldurulması zorunludur.")]
    public int UnitId { get; set; }
}
