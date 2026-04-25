using Microsoft.AspNetCore.Builder;

namespace ZeusApp.WebApi.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "ZeusApp.WebApi");
            options.RoutePrefix = "";
            options.DisplayRequestDuration();
        });
    }
}
