using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using ZeusApp.Application.Enums;

namespace ZeusApp.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Title { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public UserType UserType { get; set; }
    public bool IsActive { get; set; } = false;
    public bool RegistrationCompleted { get; set; }

    [JsonIgnore]
    public override string PasswordHash
    {
        get => base.PasswordHash;
        set => base.PasswordHash = value;
    }

    [JsonIgnore]
    public override string SecurityStamp
    {
        get => base.SecurityStamp;
        set => base.SecurityStamp = value;
    }

    [JsonIgnore]
    public override string ConcurrencyStamp
    {
        get => base.ConcurrencyStamp;
        set => base.ConcurrencyStamp = value;
    }
}
