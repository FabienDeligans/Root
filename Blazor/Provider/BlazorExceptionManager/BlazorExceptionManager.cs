using System.Net;
using Newtonsoft.Json;

namespace Blazor.Provider.BlazorExceptionManager
{
    public class BlazorExceptionManager
    {
        public async Task<string> CatchExceptions(Exception exception, HttpResponseMessage response )
        {
            if (response is null)
            {
                return await CatchResponseNull(exception, response);
            }

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return await CatchBadRequest(exception, response);
                    break;
                case HttpStatusCode.Unauthorized:
                    return await CatchUnauthorized(exception, response);
                    break;
            }

            return exception.Message;
        }

        private async Task<string> CatchUnauthorized(Exception e, HttpResponseMessage response)
        {
            return await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);
        }

        private async Task<string> CatchResponseNull(Exception e, HttpResponseMessage response)
        {
            return e.Message;
        }

        private async Task<string> CatchBadRequest(Exception e, HttpResponseMessage response)
        {
            var returnJson = await response.Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);

            var errors = JsonConvert.DeserializeObject<ProblemDetailsWithErrors>(returnJson).Errors;

            string msg = "";
            foreach (var error in errors)
            {
                msg += $"{error.Key} : ";
                for (var i = 0; i < errors.Values.Count; i++)
                {
                    msg += $"{error.Value[i]} - ";
                }
            }

            return msg;
        }
    }
}
