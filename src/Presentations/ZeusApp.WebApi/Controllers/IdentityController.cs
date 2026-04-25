using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.DTOs.Identity;
using ZeusApp.Application.Interfaces;

namespace ZeusApp.WebApi.Controllers;

[Route("api/identity"), ApiController]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost("token"), AllowAnonymous]
    public async Task<IActionResult> GetTokenAsync(TokenRequest tokenRequest)
    {
        var ipAddress = GenerateIPAddress();
        var token = await _identityService.GetTokenAsync(tokenRequest, ipAddress);
        return Ok(token);
    }

    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        var origin = Request.Headers["origin"];
        return Ok(await _identityService.RegisterAsync(request, origin));
    }

    [HttpGet("confirm-email"), AllowAnonymous]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
    {
        return Ok(await _identityService.ConfirmEmailAsync(userId, code));
    }

    [HttpPost("forgot-password"), AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
    {
        await _identityService.ForgotPassword(model, Request.Headers["origin"]);
        return Ok();
    }

    [HttpPost("reset-password"), AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
    {
        return Ok(await _identityService.ResetPassword(model));
    }

    private string GenerateIPAddress()
    {
        return Request.Headers.ContainsKey("X-Forwarded-For") ? Request.Headers["X-Forwarded-For"] : HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}
