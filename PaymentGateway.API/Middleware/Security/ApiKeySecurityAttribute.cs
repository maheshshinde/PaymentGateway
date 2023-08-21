using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace PaymentGateway.API.Middleware.Security
{
    public class ApiKeySecurityAttribute : Attribute, IAsyncActionFilter
    {
        private const string API_KEY = "Api_Key";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool success = context.HttpContext.Request.Headers.TryGetValue(API_KEY, out var apiKeyFromHttpHeader);

            if (!success)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Please Provide an API key to access this API endpoint"
                };

                return;
            }

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration Configuration = configurationBuilder.Build();

            string api_key_From_Configuration = Configuration[API_KEY];

            if (!api_key_From_Configuration.Equals(apiKeyFromHttpHeader))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized : The Api key is incorrect."
                };

                return;
            }

            await next();
        }
    }
}
