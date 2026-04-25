using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeusApp.Application.DTOs.Stock;
public class StockOutProductCreateRequest
{
    public int ProductServiceId { get; set; }

    [Display(Name = "Stok Miktarı")]
    public decimal StockAmount { get; set; }

    [Display(Name = "Birimi")]
    public int UnitId { get; set; }
}
