using System.Net;
using Library.Models;
using Newtonsoft.Json;

namespace Library.Blazor.CallApiAddressProvider
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
                var msg = e.Message;
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = e.Message;
                throw new Exception(msg);
            }
        }
    }
}
