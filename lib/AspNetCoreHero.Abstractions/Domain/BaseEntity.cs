using System.ComponentModel.DataAnnotations;
using AspNetCoreHero.Abstractions.Enums;

namespace AspNetCoreHero.Abstractions.Domain;

/// <summary>
/// Temel varlık sınıfı
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Varlık numarası
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Varlık durumu
    /// </summary>
    [Required]
    public EntityStatus Status { get; set; } = EntityStatus.Active;

   
    /// <summary>
    /// Gösterim için sıra numarası
    /// </summary>
    [Required]
    public int? DisplayOrder { get; set; } = 0;
}
