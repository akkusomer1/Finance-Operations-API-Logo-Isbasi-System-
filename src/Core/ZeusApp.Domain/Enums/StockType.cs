using System.ComponentModel.DataAnnotations;

namespace ZeusApp.Domain.Enums;
public enum StockType
{
    [Display(Name = "Ürün Giriş")]
    stockIn,

    [Display(Name = "Ürün Çıkış")]
    stockOut
}

