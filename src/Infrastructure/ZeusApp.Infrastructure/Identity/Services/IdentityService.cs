using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.Results;
using AspNetCoreHero.ThrowR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ZeusApp.Application.DTOs.Identity;
using ZeusApp.Application.DTOs.Mail;
using ZeusApp.Application.DTOs.Settings;
using ZeusApp.Application.Enums;
using ZeusApp.Application.Exceptions;
using ZeusApp.Application.Interfaces;
using ZeusApp.Application.Interfaces.Shared;
using ZeusApp.Infrastructure.Identity.Models;

namespace ZeusApp.Infrastructure.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JWTSettings _jwtSettings;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMailService _mailService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JWTSettings> jwtSettings,
        IDateTimeService dateTimeService,
        SignInManager<ApplicationUser> signInManager,
        IMailService mailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings.Value;
        _dateTimeService = dateTimeService;
        _signInManager = signInManager;
        _mailService = mailService;
    }

    public async Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        Throw.Exception.IfNull(user, nameof(user), "Bilgiler hatali.");

        var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, true);

        Throw.Exception.IfFalse(user.EmailConfirmed, $"E-posta adresi dogrulanmadi: '{request.Email}'.");
        Throw.Exception.IfFalse(user.IsActive, "Bilgiler hatali");
        Throw.Exception.IfFalse(result.Succeeded, "Bilgiler hatali");

        var jwtSecurityToken = await GenerateJWToken(user, ipAddress);

        var abilities = new List<Ability> {
            new() { Subject = "Authenticated", Action = "read" },
            new() { Subject = "ACL", Action = "read" }
        };

        var roleNames = await _userManager.GetRolesAsync(user);
        if (roleNames.Count > 0)
        {
            var role = await _roleManager.FindByNameAsync(roleNames[0]);
            if (role != null)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                abilities.AddRange(claims.Select(claim => new Ability { Subject = claim.Type, Action = claim.Value }));
            }
        }

        var response = new TokenResponse
        {
            Id = user.Id,
            JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime(),
            ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime(),
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Ability = abilities,
            UserType = user.UserType,
            Role = user.UserType.ToString()
        };

        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        response.Roles = rolesList.ToList();
        response.IsVerified = user.EmailConfirmed;

        var refreshToken = GenerateRefreshToken(ipAddress);
        response.RefreshToken = refreshToken.Token;

        return Result<TokenResponse>.Success(response, "Authenticated", 200);
    }

    private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user, string ipAddress)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(t => new Claim("roles", t)).ToList();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id),
            new Claim("first_name", user.FirstName),
            new Claim("last_name", user.LastName),
            new Claim("full_name", $"{user.FirstName} {user.LastName}"),
            new Claim("ip", ipAddress),
            new Claim("kullanici_turu", user.UserType.ToString())
        }
        .Union(userClaims)
        .Union(roleClaims);

        return JWTGeneration(claims);
    }

    private JwtSecurityToken JWTGeneration(IEnumerable<Claim> claims)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials);

        return jwtSecurityToken;
    }

    private string RandomTokenString()
    {
        using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        var randomBytes = new byte[40];
        rngCryptoServiceProvider.GetBytes(randomBytes);
        // convert random bytes to hex string
        return BitConverter.ToString(randomBytes).Replace("-", "");
    }

    private RefreshToken GenerateRefreshToken(string ipAddress)
    {
        return new()
        {
            Token = RandomTokenString(),
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress,
        };
    }

    public async Task<Result<TokenResponse>> RegisterAsync(RegisterRequest request, string origin)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithSameEmail != null)
        {
            throw new ApiException($"Email {request.Email} is already registered.");
        }

        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.Email,
            UserType = UserType.ZeusApp,
            IsActive = true,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new ApiException($"{result.Errors}");
        }

        var jwtSecurityToken = await GenerateJWToken(user, "127.0.0.1");

        var abilities = new List<Ability>
        {
            new() {Subject = "Authenticated", Action = "read"},
            new() {Subject = "ACL", Action = "read"}
        };

        var roleNames = await _userManager.GetRolesAsync(user);
        if (roleNames.Count > 0)
        {
            var role = await _roleManager.FindByNameAsync(roleNames[0]);
            if (role != null)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                abilities.AddRange(claims.Select(claim => new Ability { Subject = claim.Type, Action = claim.Value }));
            }
        }

        var response = new TokenResponse
        {
            Id = user.Id,
            JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            IssuedOn = jwtSecurityToken.ValidFrom.ToLocalTime(),
            ExpiresOn = jwtSecurityToken.ValidTo.ToLocalTime(),
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Ability = abilities,
            UserType = UserType.ZeusApp,
            Role = UserType.ZeusApp.ToString(),
        };

        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
        response.Roles = rolesList.ToList();
        response.IsVerified = user.EmailConfirmed;

        var refreshToken = GenerateRefreshToken("127.0.0.1");
        response.RefreshToken = refreshToken.Token;

        return Result<TokenResponse>.Success(response, "Registered", 200);
    }

    private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var route = "api/identity/confirm-email/";

        var _enpointUri = new Uri(string.Concat($"{origin}/", route));

        var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);

        verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);

        return verificationUri;
    }

    public async Task<Result<string>> ConfirmEmailAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);

        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded)
        {
            return Result<string>.Success(user.Id, $"Account Confirmed for {user.Email}. You can now use the /api/identity/token endpoint to generate JWT.", 200);
        }

        throw new ApiException($"An error occured while confirming {user.Email}.");
    }

    public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
    {
        var account = await _userManager.FindByEmailAsync(model.Email);
        if (account == null) return;

        var code = await _userManager.GeneratePasswordResetTokenAsync(account);
        var route = "api/identity/reset-password/";
        var _enpointUri = new Uri(string.Concat($"{origin}/", route));
        var emailRequest = new MailRequest()
        {
            Body = $"You reset token is - {code}",
            To = model.Email,
            Subject = "Reset Password",
        };
        await _mailService.SendAsync(emailRequest);
    }

    public async Task<Result<string>> ResetPassword(ResetPasswordRequest model)
    {
        var account = await _userManager.FindByEmailAsync(model.Email);
        if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");

        var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);
        if (result.Succeeded)
        {
            return Result<string>.Success(model.Email, $"Password Resetted.", 200);
        }

        throw new ApiException($"Error occured while reseting the password.");
    }
}
