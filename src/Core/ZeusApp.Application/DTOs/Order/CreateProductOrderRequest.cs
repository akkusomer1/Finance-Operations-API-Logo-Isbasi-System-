using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Entities.Catalog;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.DTOs.Order;
public class CreateProductOrderRequest
{

    /// <summary>
    /// Ürün-Hizmet Miktarı
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


    /// <summary>
    /// Toplam  Tutarı (Herhangi para birimi)
    /// Ürün Miktarı X  Birim Fiyat=Toplam Satış Tutarı
    /// </summary>
    public decimal TotalSalesAmountForProduct { get; set; }

    /// <summary>
    /// Temin Tarihi
    /// </summary>
    public DateTime SupplyDate { get; set; }

    /// <summary>
    /// Sevk Edilen Miktar
    /// </summary>
    public decimal ShippedQuantity { get; set; }

    /// <summary>
    /// Bekleyen Miktar

    /// </summary>
    public decimal PendingQuantity { get; set; }

    /// <summary>
    /// Açıklama
    /// </summary>
    public string? Description { get; set; }

    public int ProductServiceId { get; set; }
    public int UnitId { get; set; }
}
