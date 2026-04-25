using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Services.Queries.GetById;
public class GetServiceByIdResponse
{
    public int Id { get; set; }
    public int Status { get; set; }

    /// <summary>
    /// Kodu
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Hizmet Ad
    /// </summary>
    public string Name { get; set; } = null!;


    /// <summary>
    /// KDV Oranı (%)
    /// </summary>
    public int VATRate { get; set; }


    /// <summary>
    /// Para Birimi
    /// </summary>
    public CurrencyType CurrencyType { get; set; }


    /// <summary>
    /// SATIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal SalesPriceIncludingVAT { get; set; }

    /// <summary>
    /// SATIŞ Birim Fiyat (KDV Hariç)
    /// </summary>
    public decimal SalesUnitPriceExcludingVAT { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal PurchasePriceIncludingVAT { get; set; }

    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Hariç)
    /// </summary>
    public decimal PurchasePriceExcludingVAT { get; set; }


    //Navigation

    /// <summary>
    /// Hizmet Grubu
    /// </summary>
    public int? ServiceGroupId { get; set; }

    /// <summary>
    /// Hizmet kategori
    /// </summary>
    public int? ServiceCategoryId { get; set; }

    /// <summary>
    /// Birimi 
    /// </summary>
    /// 
    public int? UnitId { get; set; }
}
