using System.Collections.Generic;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;

public class ProductServiceCategory : AuditableEntity
{
    /// <summary>
    /// Ürün Kategori Adı
    /// </summary>
    public string Name { get; set; } = null!;

    public ProductServiceType ProductServiceType { get; set; }
    public ICollection<ProductService> ProductServices { get; set; } = new HashSet<ProductService>();
}
