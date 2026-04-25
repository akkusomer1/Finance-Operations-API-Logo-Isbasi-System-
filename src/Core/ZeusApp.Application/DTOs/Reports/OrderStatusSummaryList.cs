using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZeusApp.Application.DTOs.Reports;
public class OrderStatusSummaryList
{
    public int Id { get; set; }

    /// <summary>
    /// "Sipariş Tarihi"
    /// </summary> 
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// "Sipariş Numarası"
    /// </summary> 
    public string OrderNumber { get; set; }

    /// <summary>
    /// "Müşteri/Tedarikçi"
    /// </summary> 
    public string Customer { get; set; }

    /// <summary>
    /// "Toplam"
    /// </summary> 
    public decimal Total { get; set; }

    /// <summary>
    /// "Sipariş Durumu"
    /// </summary> 
    public string OrderStatus { get; set; }

    /// <summary>
    /// "Ödeme Durumu"
    /// </summary> 
    public string PaymentStatus { get; set; }


}
