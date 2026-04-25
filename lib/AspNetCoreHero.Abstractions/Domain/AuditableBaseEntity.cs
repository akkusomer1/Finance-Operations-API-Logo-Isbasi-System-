using System;

namespace AspNetCoreHero.Abstractions.Domain;

/// <summary>
/// Oluşturulma ve düzenlenme bilgilerini içeren temel varlık
/// </summary>
public abstract class AuditableEntity : BaseEntity
{
    /// <summary>
    /// Oluşturan kullanıcı
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Oluşturulma tarihi
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Son düzenleyen kullanıcı
    /// </summary>
    public string? LastModifiedBy { get; set; }

    /// <summary>
    /// Son düzenlenme zamanı
    /// </summary>
    public DateTime? LastModifiedOn { get; set; }
}
