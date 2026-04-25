using System.Collections.Generic;
using System.ComponentModel;
using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;

public class StockCategory : AuditableEntity
{

    [DisplayName("Kategori")]
    public string Name { get; set; } = null!;

    public ICollection<Stock> Stocks { get; set; } = new HashSet<Stock>();
}
