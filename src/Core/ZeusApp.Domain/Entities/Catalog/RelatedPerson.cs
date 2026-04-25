using System;
using AspNetCoreHero.Abstractions.Domain;

namespace ZeusApp.Domain.Entities.Catalog;

/// <summary>
/// Müşteri ve tedarikçi eklerken diğer kişiler alanına ait tablo
/// </summary>
public class RelatedPerson : AuditableEntity
{
    /// <summary>
    /// Ad Soyad
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Telefon
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// E-posta
    /// </summary>
    public string Email { get; set; }

    public int CustomerSupplierId { get; set; }
    public CustomerSupplier CustomerSupplier { get; set; }
}
