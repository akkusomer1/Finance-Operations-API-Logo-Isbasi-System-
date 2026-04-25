using System.Threading.Tasks;
using AspNetCoreHero.Results;
using ZeusApp.Application.DTOs.Identity;

namespace ZeusApp.Application.Interfaces;

public interface IIdentityService
{
    Task<Result<TokenResponse>> GetTokenAsync(TokenRequest request, string ipAddress);
    Task<Result<TokenResponse>> RegisterAsync(RegisterRequest request, string origin);
    Task<Result<string>> ConfirmEmailAsync(string userId, string code);
    Task ForgotPassword(ForgotPasswordRequest model, string origin);
    Task<Result<string>> ResetPassword(ResetPasswordRequest model);
}
