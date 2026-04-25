using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using MediatR;

namespace ZeusApp.Application.DTOs.Stock;
public class ProductStockResponse
{
    public int ProductServiceId { get; set; }

    [Display(Name = "Stok Miktar")]
    public decimal StockAmount { get; set; }


    [Display(Name = "Birimi")]
    public int UnitId { get; set; }


    [Display(Name = "ALIŞ Birim Fiyat (KDV Hariç)")]
    public decimal PurchasePriceExcludingVAT { get; set; }


    [Display(Name = "Toplam Tutar")]
    public decimal TotalAmount { get; set; }
}
