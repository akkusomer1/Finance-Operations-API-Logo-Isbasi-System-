namespace ZeusApp.Application.DTOs.Identity;

public class TokenRequest
{
    /// <summary>
    /// E-posta adresi
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Parola
    /// </summary>
    public string Password { get; set; }
}
