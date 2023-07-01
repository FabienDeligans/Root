using System.Net;
using Blazor.Provider.BlazorExceptionManager;
using Blazor.Provider.LoraineProvider.Models;
using Newtonsoft.Json;

namespace Blazor.Provider.LoraineProvider
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
