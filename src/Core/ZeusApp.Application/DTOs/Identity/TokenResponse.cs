using System;
using System.Collections.Generic;
using ZeusApp.Application.Enums;

namespace ZeusApp.Application.DTOs.Identity;

public class TokenResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public bool IsVerified { get; set; }
    public string JWToken { get; set; }
    public DateTime IssuedOn { get; set; }
    public DateTime ExpiresOn { get; set; }
    public string RefreshToken { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Ability> Ability { get; set; }
    public UserType UserType { get; set; }
    public string Role { get; set; }
}

public class Ability
{
    public string Action { get; set; }
    public string Subject { get; set; }
}