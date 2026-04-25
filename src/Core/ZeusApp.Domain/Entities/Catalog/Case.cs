using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;
public class Case : AuditableEntity
{
    /// <summary>
    /// Kasa Adı
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Döviz
    /// </summary>
    public CurrencyType Currency { get; set; }

    /// <summary>
    /// Açılış bakiyesi
    /// </summary>
    public decimal OpeningBalance{ get; set; }

    /// <summary>
    /// Açılış bakiyesi Tarihi
    /// </summary>
    public DateTime OpeningBalanceDate { get; set; }
}
