using Blazor.Provider.AddressProvider.Models;
using Newtonsoft.Json;

namespace Blazor.Provider.AddressProvider
{
    public class ApiAddressProvider
    {
        protected readonly HttpClient _httpClient;
        protected HttpResponseMessage? Response { get; set; }
        protected readonly BlazorExceptionManager.BlazorExceptionManager BlazorExceptionManager;

        public ApiAddressProvider(HttpClient client, BlazorExceptionManager.BlazorExceptionManager blazorExceptionManager)
        {
            _httpClient = client;
            _httpClient.BaseAddress = new Uri($"https://api-adresse.data.gouv.fr/search/");
            BlazorExceptionManager = blazorExceptionManager;
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
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }
    }
}
