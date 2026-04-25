using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZeusApp.Application.DTOs.Identity;
using ZeusApp.Application.Enums;
using ZeusApp.Application.Exceptions;
using ZeusApp.Infrastructure.Identity.Models;
using static System.Enum;

namespace ZeusApp.WebApi.Controllers.v2;

public class UserController : BaseApiController<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string role, int? status)
    {
        var user = await _userManager.GetUserAsync(User);
        var users = _userManager.Users;

        if (user.UserType == UserType.ZeusApp)
        {
            if (role != null)
            {
                if (TryParse(role, true, out UserType kullaniciTuru))
                {

                }

                users = users.Where(w => w.UserType == kullaniciTuru);
            }

            if (status != null)
            {
                var isStatus = Convert.ToBoolean(status);
                users = users.Where(w => w.IsActive == isStatus);
            }

            return Ok(users);
        }

        if (status != null)
        {
            var isStatus = Convert.ToBoolean(status);
            users = users.Where(w => w.IsActive == isStatus);
        }

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RegisterRequest request)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithSameEmail == null)
        {
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email,
                Title = request.Title,
                IsActive = true,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return Ok(user.Id);
            }

            throw new ApiException($"{result.Errors}");
        }

        throw new ApiException($"Bu {request.Email} hesabı zaten mevcut!");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, ApplicationUser command)
    {
        var loggedUser = await _userManager.GetUserAsync(User);
        var user = await _userManager.FindByIdAsync(id);

        user.FirstName = command.FirstName;
        user.LastName = command.LastName;
        user.Email = command.Email;
        user.Title = command.Title;
        user.PhoneNumber = command.PhoneNumber;

        if (loggedUser.UserType == UserType.ZeusApp)
        {
            user.IsActive = command.IsActive;
            user.UserType = command.UserType;
        }

        var response = await _userManager.UpdateAsync(user);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        IdentityResult result = null;
        try
        {
            var user = await _userManager.FindByIdAsync(id);
            result = await _userManager.DeleteAsync(user);
        }
        catch (Exception)
        {

        }

        return Ok(result);
    }
}
