using System.Net.Mime;
using System.Text;
using Common.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Exception = System.Exception;
using Route = Common.Route; 


namespace Blazor.Provider.Api.CallApiProviderBase
{
    public class BaseCallApi<T> : ICallApi<T> where T : IEntity
    {
        protected readonly HttpClient _httpClient;
        protected readonly IOptions<SettingsCallApi> _options;
        protected readonly BlazorExceptionManager.BlazorExceptionManager BlazorExceptionManager; 
        protected HttpResponseMessage? Response { get; set; }

        public BaseCallApi(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager.BlazorExceptionManager blazorExceptionManager)
        {
            _options = options;
            _httpClient = client;
            _httpClient.DefaultRequestHeaders.Add("From", _options.Value.CallerName);
            _httpClient.BaseAddress = new Uri($"{_options.Value.Adress}{GetTypeName()}/");
            BlazorExceptionManager = blazorExceptionManager;
        }

        public string GetTypeName() => typeof(T).Name;

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

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8,
                    MediaTypeNames.Application.Json);

                Response = await _httpClient
                    .PostAsync(Route.CreateAsync, json)
                    .ConfigureAwait(false);

                Response.EnsureSuccessStatusCode();

                var returnJson = await Response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<T>(returnJson);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(entities), Encoding.UTF8, MediaTypeNames.Application.Json);

                Response = await _httpClient
                    .PostAsync(Route.CreateManyAsync, json)
                    .ConfigureAwait(false);

                Response.EnsureSuccessStatusCode();

                var returnJson = await Response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<IEnumerable<T>>(returnJson);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<IEnumerable<T>?> GetAllAsync()
        {
            try
            {
                return await _httpClient
                        .GetFromJsonAsync<IEnumerable<T>>(Route.GetAllAsync)
                        .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<IEnumerable<T>?> GetAllFilteredByPropertyEqualAsync(string propertyName, string value)
        {
            try
            {
                return await _httpClient
                    .GetFromJsonAsync<IEnumerable<T>>($"{Route.GetAllFilteredByPropertyEqualAsync}/{propertyName}/{value}")
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<T?> GetOneFullAsync(string id)
        {
            try
            {
                return await _httpClient
                        .GetFromJsonAsync<T>($"{Route.GetOneFullAsync}/{id}")
                        .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<T?> GetOneSimpleAsync(string id)
        {
            try
            {
                return await _httpClient
                        .GetFromJsonAsync<T>($"{Route.GetOneSimpleAsync}/{id}")
                        .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task DeleteOneAsync(string id)
        {
            try
            {
                Response = await _httpClient
                        .DeleteAsync($@"{Route.DeleteOneAsync}/{id}")
                        .ConfigureAwait(false);

                Response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<T> UpdateAsync(T entityUpdate)
        {
            var json = new StringContent(JsonConvert.SerializeObject(entityUpdate), Encoding.UTF8, MediaTypeNames.Application.Json);

            try
            {
                Response = await _httpClient
                        .PutAsync($@"{Route.UpdateAsync}", json)
                        .ConfigureAwait(false);

                Response.EnsureSuccessStatusCode();

                var returnJson = await Response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<T>(returnJson);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }

        public async Task<T> UpdatePropertyAsync(string id, Dictionary<string, object> propertyValueDictionary)
        {
            var json = new StringContent(JsonConvert.SerializeObject(propertyValueDictionary), Encoding.UTF8, MediaTypeNames.Application.Json);

            try
            {
                Response = await _httpClient
                    .PutAsync($@"{Route.UpdatePropertyAsync}/{id}", json)
                    .ConfigureAwait(false);

                Response.EnsureSuccessStatusCode();

                var returnJson = await Response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<T>(returnJson);
            }
            catch (Exception e)
            {
                var msg = await BlazorExceptionManager.CatchExceptions(e, Response);
                throw new Exception(msg);
            }
        }
    }
}
