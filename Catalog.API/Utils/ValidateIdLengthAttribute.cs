using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Utils
{
    public class ValidateIdLengthAttribute : ActionFilterAttribute
    {
        private readonly string _configurationKey;

        public ValidateIdLengthAttribute(string configurationKey)
        {
            _configurationKey = configurationKey;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var idLength = int.Parse(configuration.GetValue<string>(_configurationKey));

            if (context.ActionArguments.TryGetValue("id", out var idObj) && idObj is string id)
            {
                if (id.Length != idLength)
                {
                    context.Result = new BadRequestObjectResult($"The ID must be {idLength} characters long.");
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
