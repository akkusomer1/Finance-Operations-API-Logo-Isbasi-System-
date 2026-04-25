using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ZeusApp.Infrastructure.Identity.Models;

namespace ZeusApp.Infrastructure.Identity.Seeds;

public static class DefaultBasicUser
{
    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await Task.CompletedTask;
    }
}
