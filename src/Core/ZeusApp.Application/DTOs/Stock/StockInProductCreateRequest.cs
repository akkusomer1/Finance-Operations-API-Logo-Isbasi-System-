using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ZeusApp.Application.DTOs.Stock;
public class StockInProductCreateRequest
{
    public int ProductServiceId { get; set; }

    [Display(Name = "Stok Miktar")]
    public decimal StockAmount { get; set; }


    [Display(Name = "Birimi")]
    public int UnitId { get; set; }


    [Display(Name = "ALIŞ Birim Fiyat (KDV Hariç)")]
    [Precision(30, 5)]
    public decimal PurchasePriceExcludingVAT { get; set; }


    [Display(Name = "Toplam Tutar")]
    [Precision(35, 2)]
    public decimal TotalAmount { get; set; }
}
