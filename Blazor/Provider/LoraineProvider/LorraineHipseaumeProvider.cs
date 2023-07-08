using Blazor.Provider.LoraineProvider.Models;
using Newtonsoft.Json;

namespace Blazor.Provider.LoraineProvider
{
    public class LorraineHipseaumeProvider
    {
        protected readonly HttpClient _httpClient;
        protected HttpResponseMessage? Response { get; set; }
        protected readonly BlazorExceptionManager.BlazorExceptionManager BlazorExceptionManager;

        public LorraineHipseaumeProvider(HttpClient client, BlazorExceptionManager.BlazorExceptionManager blazorExceptionManager)
        {
            _httpClient = client;
            _httpClient.BaseAddress = new Uri($"https://lorraine-hipseau.me/data?rarity=0&title=0&q=2011-2020");
            BlazorExceptionManager = blazorExceptionManager;
        }
        public async Task<List<NomPrenomHipseaume>> GetListRandomName()
        {
            try
            {
                Response = await _httpClient.GetAsync("");
                var returnJson = await Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<ListNomPrenomLorraineHipseaume>(returnJson);

                var prenomNomList = new List<NomPrenomHipseaume>();
                foreach (var hipseaume in result.Results)
                {
                    prenomNomList.Add(new NomPrenomHipseaume()
                    {
                        Nom = hipseaume.Nom,
                        Prenom = hipseaume.Prenom,
                        Sexe = hipseaume.Sexe,
                    });
                }

                return prenomNomList; 
            }
            catch (Exception e) when (Response is null)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }
    }
}
