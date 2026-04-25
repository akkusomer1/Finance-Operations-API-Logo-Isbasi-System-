using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ZeusApp.Application.Constants;
using ZeusApp.Application.Enums;
using ZeusApp.Infrastructure.Identity.Models;

namespace ZeusApp.Infrastructure.Identity.Seeds;

public static class DefaultRoles
{
    public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        var allPermissions = Permissions.GeneratePermissionsForModule(module);

        foreach (var permission in allPermissions)
        {
            if (!allClaims.Any(claim => claim.Type == "Permission" && claim.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
            }
        }
    }

    private static async Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync(Role.SuperAdmin.ToString());
        var adminAllClaims = await roleManager.GetClaimsAsync(adminRole);

        if (adminAllClaims.All(claim => claim.Type != "superadmin-dashboard"))
        {
            await roleManager.AddClaimAsync(adminRole, new Claim("superadmin-dashboard", "read"));
        }
    }

    private static async Task SeedClaimsForFirmaAdmin(this RoleManager<IdentityRole> roleManager)
    {
        var firmaRole = await roleManager.FindByNameAsync(Role.Admin.ToString());
        var firmaAllClaims = await roleManager.GetClaimsAsync(firmaRole);

        if (firmaAllClaims.All(claim => claim.Type != "firma-dashboard"))
        {
            await roleManager.AddClaimAsync(firmaRole, new Claim("firma-dashboard", "read"));
        }
    }

    public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Role.SuperAdmin.ToString()));
        await roleManager.SeedClaimsForSuperAdmin();

        await roleManager.CreateAsync(new IdentityRole(Role.Admin.ToString()));
        await roleManager.SeedClaimsForFirmaAdmin();
    }
}
