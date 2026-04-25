namespace AspNetCoreHero.Abstractions.Enums;

/// <summary>
/// Varlık durumu
/// </summary>
public enum EntityStatus
{
    /// <summary>
    /// Varlık silindi // sistemde gözükmez
    /// </summary>
    Deleted = -1,
    /// <summary>
    /// Varlık pasif // admin görebilir - aktif edebilir
    /// </summary>
    Passive = 0,
    /// <summary>
    /// Varlık aktif
    /// </summary>
    Active = 1,
}
