using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ZeusApp.WebApi.Filters;

public class ValidateFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errorMessage = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).First();

            context.Result = new BadRequestObjectResult(Result<List<string>>.Fail(errorMessage, 400));
        }
        base.OnActionExecuting(context);
    }
}
