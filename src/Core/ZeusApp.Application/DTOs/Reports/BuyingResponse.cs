using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeusApp.Application.DTOs.Rapors;
public class BuyingResponse
{
    public int Id { get; set; }

    /// <summary>
    /// "Tarih"
    /// </summary> 
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// "İşlem Türü"
    /// </summary> 
    public string OperationType { get; set; }

    /// <summary>
    /// "Ad-Soyad"
    /// </summary> 
    public string Name { get; set; }

    /// <summary>
    /// "Kategori"
    /// </summary> 
    public string Category { get; set; }

    /// <summary>
    /// "Fatura Numarası"
    /// </summary> 
    public int InvoiceNumber { get; set; }

    /// <summary>
    /// "Vade Tarihi"
    /// </summary> 
    public DateTime ExpiryDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// "Açıklamalar"
    /// </summary> 
    public string Descriptions { get; set; }

    /// <summary>
    /// "Tutar"
    /// </summary> 
    public decimal Amount { get; set; }



}
