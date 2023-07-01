using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Blazor.Provider.Api.CallApiProvider;
using Common.Models.Processes;
using Microsoft.Extensions.Options;
using Route = Common.Route;

namespace Blazor.Provider.Api
{
    public class ProcessProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<SettingsCallApi> _options;
        private readonly BlazorExceptionManager.BlazorExceptionManager _blazorExceptionManager;
        private HttpResponseMessage? Response { get; set; }

        public ProcessProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager.BlazorExceptionManager blazorExceptionManager)
        {
            _options = options;
            _httpClient = client;
            _httpClient.DefaultRequestHeaders.Add("From", _options.Value.CallerName);
            _httpClient.BaseAddress = new Uri($"{_options.Value.Adress}{GetTypeName()}/");
            _blazorExceptionManager = blazorExceptionManager;
        }

        private string GetTypeName() => nameof(Process);

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
                var msg = await _blazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<IEnumerable<Process>?> GetAllAsync()
        {
            try
            {
                return await _httpClient
                    .GetFromJsonAsync<IEnumerable<Process>>(Route.GetAllAsync)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var msg = await _blazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task CreateSpecificProcess(Process entity)
        {
            try
            {
                var json = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8,
                    MediaTypeNames.Application.Json);

                Response = await _httpClient
                    .PostAsync(Route.CreateSpecificProcess, json)
                    .ConfigureAwait(false);

                Response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                var msg = await _blazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task RunSpecificProcess(Process entity)
        {
            try
            {
                var json = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8,
                    MediaTypeNames.Application.Json);

                Response = await _httpClient
                    .PostAsync(Route.RunSpecificProcess, json)
                    .ConfigureAwait(false);

                Response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                var msg = await _blazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }
    }
}
