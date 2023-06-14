using System.Net;
using Blazor.Provider.AddressProvider.Models;
using Front.BlazorExceptionManager;
using Newtonsoft.Json;

namespace Blazor.Provider.AddressProvider
{
    public class ApiAddressProvider
    {
        protected readonly HttpClient _httpClient;
        protected HttpResponseMessage? Response { get; set; }

        public ApiAddressProvider(HttpClient client)
        {
            _httpClient = client;
            _httpClient.BaseAddress = new Uri($"https://api-adresse.data.gouv.fr/search/");
        }
        public async Task<AddressResult> GetListAddress(string address, int nbResult)
        {
            try
            {
                Response = await _httpClient.GetAsync($"?q={address}&limit={nbResult}");
                var returnJson = await Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<AddressResult>(returnJson);
                return result; 
            }
            catch (Exception e) when (Response is null)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var returnJson = await Response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                var errors = JsonConvert.DeserializeObject<ProblemDetailsWithErrors>(returnJson).Errors;

                string msg = "";
                foreach (var error in errors)
                {
                    msg += $"{error.Key} : ";
                    for (var i = 0; i < errors.Values.Count; i++)
                    {
                        var end = errors.Values.Count - 1 == i ? "" : "- ";
                        msg += $"{error.Value[i]} {end}";
                    }
                }
                throw new Exception(msg);
            }
        }
    }
}
