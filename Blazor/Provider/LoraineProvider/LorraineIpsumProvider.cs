using Blazor.Provider.LoraineProvider.Models;
using Newtonsoft.Json;

namespace Blazor.Provider.LoraineProvider
{
    public class LorraineIpsumProvider
    {
        protected readonly HttpClient _httpClient;
        protected HttpResponseMessage? Response { get; set; }
        protected readonly BlazorExceptionManager.BlazorExceptionManager BlazorExceptionManager;

        public LorraineIpsumProvider(HttpClient client, BlazorExceptionManager.BlazorExceptionManager blazorExceptionManager)
        {
            _httpClient = client;
            _httpClient.BaseAddress = new Uri($"https://www.lorraine-ipsum.fr/frontend/ajax_get_results?random=1683491757900");
            BlazorExceptionManager = blazorExceptionManager;
        }
        public async Task<List<NomPrenomIpsum>> GetListRandomName()
        {
            try
            {
                Response = await _httpClient.GetAsync("");
                var returnJson = await Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<IEnumerable<NomPrenomLorraineIpsum>>(returnJson);

                var nomPrenomList = new List<NomPrenomIpsum>();
                foreach (var ipsum in result)
                {
                    nomPrenomList.Add(new NomPrenomIpsum()
                    {
                        Prenom = ipsum.name_part,
                        Nom = ipsum.word_part
                    });
                }
                return nomPrenomList;
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }
    }
}
