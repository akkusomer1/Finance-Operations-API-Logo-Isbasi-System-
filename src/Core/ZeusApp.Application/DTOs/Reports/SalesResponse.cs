using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZeusApp.Application.DTOs.Rapors;
public class SalesResponse
{
    public int Id { get; set; }

    /// <summary>
    /// "Tarih "
    /// </summary> 
    public DateTime Date { get; set; } = DateTime.UtcNow;
    /// <summary>
    /// "Toplam Tutar "
    /// </summary> 
   
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// "İndirim Oranı "
    /// </summary> 
    public int DiscountRate { get; set; }

    /// <summary>
    /// "Tutar "
    /// </summary> 
    public decimal Amount { get; set; }

    /// <summary>
    /// "Kategori "
    /// </summary> 
    public string Category { get; set; }

    /// <summary>
    /// "Toplam Kdv Tutarı "
    /// </summary> 
    public decimal TotalVatAmount { get; set; }

    /// <summary>
    /// "Net Toplam "
    /// </summary> 
    public decimal NetTotal { get; set; }

    /// <summary>
    /// "Toplam Fatura Sayısı "
    /// </summary> 
    public int TotalNumberOfInvoices { get; set; }
}
