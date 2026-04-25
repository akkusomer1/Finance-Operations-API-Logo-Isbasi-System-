using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.DTOs.Invoice;
public class ProductInvoiceUpdateRequest
{
    public int Id { get; set; }
    /// <summary>
    /// Ürün Miktarı
    /// </summary>
    public decimal ProductAmount { get; set; }

    /// <summary>
    /// Birim Fiyat
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Vergi Oranı Yüzde % KDV Oranı
    /// </summary>
    public decimal TaxRate { get; set; }

    /// <summary>
    /// Vergi Tutarı
    /// </summary>
    public decimal TaxAmount { get; set; }

    /// <summary>
    /// İndirim
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// İndirim Tipi
    /// </summary>
    public DiscountType DiscountType { get; set; }


    public string? Description { get; set; }

    /// <summary>
    /// Toplam  Tutarı (Herhangi para birimi)
    /// Ürün Miktarı X  Birim Fiyat=Toplam Satış Tutarı
    /// </summary>
    public decimal TotalSalesAmountForProduct { get; set; }

    [Required(ErrorMessage ="ssss delikanlı")]
    public int ProductServiceId { get; set; }

    public int UnitId { get; set; }

    /// <summary>
    /// Son Kullanma Tarihi
    /// </summary>
    public string? ExpirationDate { get; set; }

    /// <summary>
    /// SerialOrLotNumber
    /// </summary>
    public string? SerialOrLotNumber { get; set; }
}
