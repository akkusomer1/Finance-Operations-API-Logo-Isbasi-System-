using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZeusApp.Infrastructure.DbContexts;
using ZeusApp.Infrastructure.Identity.Models;

namespace ZeusApp.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            
            // Begin auto migrate
            //using var db = services.GetRequiredService<ApplicationDbContext>();
            //db.Database.Migrate();

            //using var identityDb = services.GetRequiredService<IdentityContext>();
            //identityDb.Database.Migrate();
            // End auto migrate

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("app");

            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                await Infrastructure.Identity.Seeds.DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
                await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);

                //var mediator = services.GetService<IMediator>();
                //await Infrastructure.Seeds.DefaultUlke.SeedAsync(mediator);

                logger.LogInformation("Finished Seeding Default Data");
                logger.LogInformation("Application Starting");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred seeding the DB");
            }
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}
