using Library.Models;
using System.Net;
using Newtonsoft.Json;

namespace Library.Blazor.LoraineProvider
{
    public class LorraineIpsumProvider
    {
        protected readonly HttpClient _httpClient;
        protected HttpResponseMessage? Response { get; set; }

        public LorraineIpsumProvider(HttpClient client)
        {
            _httpClient = client;
            _httpClient.BaseAddress = new Uri($"https://www.lorraine-ipsum.fr/frontend/ajax_get_results?random=1683491757900");
        }
        public async Task<IEnumerable<NomPrenomLorraineIpsum>> GetListRandomName()
        {
            try
            {
                Response = await _httpClient.GetAsync("");
                var returnJson = await Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<IEnumerable<NomPrenomLorraineIpsum>>(returnJson);
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
