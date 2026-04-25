namespace AspNetCoreHero.Results;

/// <summary>
/// Result interface
/// </summary>
public interface IResult
{
    /// <summary>
    /// Http durum kodu
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Hata veya uyari mesaji
    /// </summary>
    string Message { get; set; }
}

/// <summary>
/// Result interface
/// </summary>
public interface IResult<out T> : IResult
{
    /// <summary>
    /// Veri
    /// </summary>
    T Data { get; }

    /// <summary>
    /// Http durum kodu
    /// </summary>
    public int StatusCode { get; set; }
}
