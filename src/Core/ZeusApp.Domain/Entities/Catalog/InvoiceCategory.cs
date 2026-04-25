using System.Collections.Generic;
using AspNetCoreHero.Abstractions.Domain;
using ZeusApp.Domain.Enums;

namespace ZeusApp.Domain.Entities.Catalog;

public class InvoiceCategory : AuditableEntity
{
    /// <summary>
    ///Satış Kategorisi Adı
    /// </summary>
    public string Name { get; set; } = null!;
    public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    public InvoiceType InvoiceType { get; set; }
}
