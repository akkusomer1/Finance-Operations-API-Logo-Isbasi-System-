using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeusApp.Application.DTOs.Rapors;
public class CostResponse
{
    public int Id { get; set; }

    /// <summary>
    /// "Tarih"
    /// </summary> 
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// "Hizmet Adı"
    /// </summary> 
    public string ServiceName { get; set; }

    /// <summary>
    /// "Kategori"
    /// </summary> 
    public string Category { get; set; }

    /// <summary>
    /// "Hizmet Grubu"
    /// </summary> 
    public string ServiceGroup { get; set; }

    /// <summary>
    /// "Hareket Türü"
    /// </summary> 
    public string TypeOfMovement { get; set; }

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
