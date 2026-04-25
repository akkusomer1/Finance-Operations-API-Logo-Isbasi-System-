using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeusApp.Application.DTOs.Rapors;
public class PurchaseAndSalesTotals
{
    public int Id { get; set; }

    /// <summary>
    /// "Ürün Kodu "
    /// </summary> 
    public string ProductCode { get; set; }

    /// <summary>
    /// "Ürün "
    /// </summary> 
    public string Product { get; set; }

    /// <summary>
    /// "Ana Birim "
    /// </summary> 
    public string MainUnit { get; set; }

    /// <summary>
    /// "Satış Adedi "
    /// </summary> 
    public int NumberofSales { get; set; }

    /// <summary>
    /// "Satış Toplamı "
    /// </summary> 
    public decimal SalesTotal { get; set; }

    /// <summary>
    /// "Satış Vergi Toplamı "
    /// </summary> 
    public decimal SalesTaxTotal { get; set; }

    /// <summary>
    /// "Satış İndirim Toplamı "
    /// </summary> 
    public decimal SalesDiscountTotal { get; set; }

    /// <summary>
    /// "Satın Alma Miktarı"
    /// </summary> 
    public int PurchaseAmount { get; set; }

    /// <summary>
    /// "Satın Alma Toplamı"
    /// </summary> 
    public decimal PurchaseTotal { get; set; }

    /// <summary>
    /// "Satın Alma Vergi Toplamı"
    /// </summary> 
    public int PurchaseTaxTotal { get; set; }

    /// <summary>
    /// "Satın Alma İndirim Toplamı"
    /// </summary> 
    public int PurchaseDiscountTotal { get; set; }






}
