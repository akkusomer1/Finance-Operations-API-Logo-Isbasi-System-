using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ZeusApp.Application.DTOs.Stock;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Stocks.Queries.GetById;
public class GetStockByIdResponse
{
    public int Id { get; set; }

    [Display(Name = "Tarih")]
    public DateTime Date { get; set; }

    [Display(Name = "Belge No")]
    [Required(ErrorMessage = "{0} alanın doldurulması zorunludur.")]
    public string DocumentNo { get; set; } = null!;

    [Display(Name = "Kategori")]
    public Guid? StockCategoryId { get; set; }

    [Display(Name = "Açıklama")]
    public string? Description { get; set; }

    [Display(Name = "Değişiklik Tarihi")]
    public DateTime? LastModifiedOn { get; set; }

    [Display(Name = "Stok Harektet Türü")]
    public MovementType MovementType { get; set; }

    [Display(Name = "Döviz")]
    public CurrencyType? Currency { get; set; }

    [Display(Name = "Döviz Kuru")]
    [Precision(10, 4)]
    public decimal ExchangeRate { get; set; }

    [Display(Name = "Genel Toplam")]
    public decimal GrandTotal { get; set; }

    public StockType StockType { get; set; }
    public List<ProductStockResponse> ProductStocks { get; set; } = new List<ProductStockResponse>();
}
