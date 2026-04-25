using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeusApp.Application.Features.Stocks.Queries.GetDropdownList;
public class GetAllStockProductsResponse
{
    public int Id { get; set; }
    public int ProductServiceId { get; set; }
    public string ProductName { get; set; } = null!;

    [Display(Name = "Birimi")]
    public int  UnitId { get; set; }
  
    public string  UnitName { get; set; }

    [Display(Name = "ALIŞ Birim Fiyat (KDV Hariç)")]
    public decimal PurchasePriceExcludingVAT { get; set; }

    [Display(Name = "Toplam Stok Miktarı")]
    public decimal TotalStockAmount { get; set; }
}
