using System.Security.Authentication;
using _ApiLayer.ApiControllerProvider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace _ApiLayer.ApiExceptionManager
{
    public class ApiExceptionManager : ControllerBase
    {
        protected List<string> CallerNames { get; set; }
        public ApiExceptionManager(IOptions<SettingsApi> options)
        {
            CallerNames = options.Value.CallerNames;
        }

        public ActionResult EnsureFromAllowed(HttpRequest request)
        {
            var requestHeaders = request.Headers;
            var from = requestHeaders["From"];

            if (!CallerNames.Contains(from))
            {
                throw new AuthenticationException("Vous n'avez pas le droit de communiquer avec cette API");
            }

            return Ok();
        }

        public ActionResult CatchExceptions(Exception e)
        {
            if (e.GetType() == typeof(AuthenticationException))
            {
                return Unauthorized(e.Message);
            }

            return BadRequest(e);
        }
    }
}
