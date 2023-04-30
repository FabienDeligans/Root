using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using Library.Interfaces;
using Library.Models;
using Library.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Exception = System.Exception;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Library.Abstract
{
    public class BaseCallApi<T> : ICallApi<T> where T : IEntity
    {
        protected readonly HttpClient _httpClient;
        protected readonly IOptions<SettingsCallApi> _options;
        protected HttpResponseMessage? Response { get; set; }

        public BaseCallApi(HttpClient client, IOptions<SettingsCallApi> options)
        {
            _options = options;
            _httpClient = client;

            _httpClient.BaseAddress = new Uri($"{_options.Value.Adress}{GetTypeName()}/");
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
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
                throw new Exception(msg);
            }
        }

        public async Task<long> CountDataAsync()
        {
            try
            {
                var response = await _httpClient
                        .GetAsync(Route.CountDataAsync)
                        .ConfigureAwait(false);

                var returnJson = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<long>(returnJson);
            }
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
                throw new Exception(msg);
            }
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                var json = new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8,
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
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
                throw new Exception(msg);
            }
            catch (Exception e)
            {
                var msg = e.ToString();
                throw new Exception(msg);
            }
        }

        public async Task<IEnumerable<T>> CreateManyAsync(IEnumerable<T> entities)
        {
            try
            {
                var json = new StringContent(JsonSerializer.Serialize(entities), Encoding.UTF8, MediaTypeNames.Application.Json);

                using var response = await _httpClient
                    .PostAsync(Route.CreateManyAsync, json)
                    .ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var returnJson = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<IEnumerable<T>>(returnJson);
            }
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
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
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
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
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
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
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
                throw new Exception(msg);
            }
        }

        public async Task DeleteOneAsync(string id)
        {
            try
            {
                using var response = await _httpClient
                        .DeleteAsync($@"{Route.DeleteOneAsync}/{id}")
                        .ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
                throw new Exception(msg);
            }
        }

        public async Task<T> UpdateAsync(T entityUpdate)
        {
            var json = new StringContent(JsonSerializer.Serialize(entityUpdate), Encoding.UTF8, MediaTypeNames.Application.Json);

            try
            {
                using var response = await _httpClient
                        .PutAsync($@"{Route.UpdateAsync}", json)
                        .ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var returnJson = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<T>(returnJson);
            }
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
                throw new Exception(msg);
            }
        }

        public async Task<T> UpdatePropertyAsync(string id, Dictionary<string, string> propertyValueDictionary)
        {
            var json = new StringContent(JsonSerializer.Serialize(propertyValueDictionary), Encoding.UTF8, MediaTypeNames.Application.Json);

            try
            {
                using var response = await _httpClient
                    .PutAsync($@"{Route.UpdatePropertyAsync}/{id}", json)
                    .ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                var returnJson = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                return JsonConvert.DeserializeObject<T>(returnJson);
            }
            catch (Exception e) when (Response is null)
            {
                var msg = await CatchError500(e);
                throw new Exception(msg);
            }
            catch (Exception e) when (Response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = await CatchError400(e);
                throw new Exception(msg);
            }
        }

        public async Task<string> CatchError500(Exception e)
        {
            return e.Message;
        }

        public async Task<string> CatchError400(Exception e)
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

            return msg;
        }
    }

    public class ProblemDetailsWithErrors
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
