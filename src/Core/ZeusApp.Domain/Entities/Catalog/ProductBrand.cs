using System.Collections.Generic;
using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;

public class ProductBrand : AuditableEntity
{
    /// <summary>
    /// Ürün Marka Adı
    /// </summary>
    public string Name { get; set; }
    public ICollection<ProductService> Products { get; set; } = new HashSet<ProductService>();
}
