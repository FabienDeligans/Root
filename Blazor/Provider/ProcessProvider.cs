using Library.Blazor.BlazorExceptionManager;
using Library.Models.Process;
using Library.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Route = Library.Settings.Route;

namespace Blazor.Provider
{
    public class ProcessProvider 
    {
        protected readonly HttpClient _httpClient;
        protected readonly IOptions<SettingsCallApi> _options;
        protected readonly BlazorExceptionManager BlazorExceptionManager;
        protected HttpResponseMessage? Response { get; set; }

        public ProcessProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager blazorExceptionManager)
        {
            _options = options;
            _httpClient = client;
            _httpClient.DefaultRequestHeaders.Add("From", _options.Value.CallerName);
            _httpClient.BaseAddress = new Uri($"{_options.Value.Adress}{nameof(ProcessModel)}/");
            BlazorExceptionManager = blazorExceptionManager;
        }

        public async Task DropCollectionAsync()
        {
            try
            {
                Response = await _httpClient
                    .DeleteAsync(Route.DropCollectionAsync)
                    .ConfigureAwait(false);

                Response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<long> CountDataAsync()
        {
            try
            {
                Response = await _httpClient
                    .GetAsync(Route.CountDataAsync)
                    .ConfigureAwait(false);

                var returnJson = await Response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<long>(returnJson);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<IEnumerable<ProcessModel>?> GetAllAsync()
        {
            try
            {
                return await _httpClient
                    .GetFromJsonAsync<IEnumerable<ProcessModel>>(Route.GetAllAsync)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }
    }
}
