using System.Net;
using Library.Models;
using Newtonsoft.Json;

namespace Library.Blazor.LoraineProvider
{
    public class LorraineHipseaumeProvider
    {
        protected readonly HttpClient _httpClient;
        protected HttpResponseMessage? Response { get; set; }

        public LorraineHipseaumeProvider(HttpClient client)
        {
            _httpClient = client;
            _httpClient.BaseAddress = new Uri($"https://lorraine-hipseau.me/data?rarity=0&title=0&q=2011-2020");
        }
        public async Task<ListNomPrenom> GetListRandomName()
        {
            try
            {
                Response = await _httpClient.GetAsync("");
                var returnJson = await Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<ListNomPrenom>(returnJson);
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
