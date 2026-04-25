using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Enums;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Application.Features.Services.Queries.GetAllPaged;
public class GetAllServicesResponse
{

    public int Id { get; set; }
    public EntityStatus Status { get; set; }

    /// <summary>
    /// Kodu
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Hizmet Ad
    /// </summary>
    public string Name { get; set; } = null!;


    /// <summary>
    /// Birimi 
    /// </summary>
    /// 
    public int? UnitId { get; set; }


    /// <summary>
    /// Hizmet Grubu
    /// </summary>
    public int? ServiceGroupId { get; set; }

    /// <summary>
    /// SATIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal SalesPriceIncludingVAT { get; set; }


    /// <summary>
    /// ALIŞ Birim Fiyat (KDV Dahil)
    /// </summary>
    public decimal PurchasePriceIncludingVAT { get; set; }


    /// <summary>
    /// KDV Oranı (%)
    /// </summary>
    public int VATRate { get; set; } 
}
