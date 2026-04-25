using System.ComponentModel.DataAnnotations;

namespace ZeusApp.Application.DTOs.Identity;

public class ForgotPasswordRequest
{
    [Required, EmailAddress]
    public string Email { get; set; }
}
