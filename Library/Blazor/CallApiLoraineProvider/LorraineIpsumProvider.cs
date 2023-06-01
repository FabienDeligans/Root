using System.Net;
using Library.Blazor.CallApiLoraineProvider.Models;
using Library.Models;
using Newtonsoft.Json;

namespace Library.Blazor.CallApiLoraineProvider
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
