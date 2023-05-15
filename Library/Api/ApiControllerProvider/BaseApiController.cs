using Amazon.SecurityToken.Model;
using Library.Api.ApiLogicProvider;
using Library.Models;
using Library.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Library.Api.ApiControllerProvider
{
    [ApiController]
    public abstract class BaseApiController<T> : ControllerBase, IApiController<T> where T : IEntity
    {
        protected readonly IApiLogic<T> _apiLogic;
        protected List<string> CallerNames { get; set; }

        public BaseApiController(IOptions<SettingsApi> options, IApiLogic<T> apiLogic)
        {
            CallerNames = options.Value.CallerNames;
            _apiLogic = apiLogic;
        }

        public virtual ActionResult EnsureFromAllowed()
        {
            var requestHeaders = Request.Headers;
            var from = requestHeaders["From"];

            if (!CallerNames.Contains(from))
            {
                throw new InvalidAuthorizationMessageException("Vous n'avez pas le droit de communiquer avec cette API");
            }

            return Ok();
        }

        public virtual ActionResult CatchExceptions(Exception e)
        {
            if (e.GetType() == typeof(InvalidAuthorizationMessageException))
            {
                return Unauthorized(e.Message);
            }

            return BadRequest(e);
        }

        [HttpDelete(Route.DropCollectionAsync)]
        public virtual async Task<ActionResult> DropCollectionAsync()
        {
            try
            {
                EnsureFromAllowed();

                await _apiLogic.DropCollectionAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpGet(Route.CountDataAsync)]
        public virtual async Task<ActionResult<long>> CountDataAsync()
        {
            try
            {
                EnsureFromAllowed();

                return await _apiLogic.CountDataAsync();
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpPost(Route.CreateAsync)]
        public virtual async Task<ActionResult<T>> CreateAsync(T entity)
        {
            try
            {
                EnsureFromAllowed();

                var result = await _apiLogic.CreateAsync(entity);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpPost(Route.CreateManyAsync)]
        public virtual async Task<ActionResult<IEnumerable<T>>> CreateManyAsync(IEnumerable<T> entities)
        {
            try
            {
                EnsureFromAllowed();

                var result = await _apiLogic.CreateManyAsync(entities);
                return new ActionResult<IEnumerable<T>>(result);
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetAllAsync)]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                EnsureFromAllowed();

                var result = await _apiLogic.GetAllAsync();
                return new ActionResult<IEnumerable<T>>(result);
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetAllFilteredByPropertyEqualAsync + "/{propertyName}/{value}")]
        public async Task<ActionResult<IEnumerable<T>?>> GetAllFilteredByPropertyEqualAsync(string propertyName, string value)
        {
            try
            {
                EnsureFromAllowed();

                var result = await _apiLogic.GetAllFilteredByPropertyEqualAsync(propertyName, value);
                return new ActionResult<IEnumerable<T>?>(result);
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetOneFullAsync + "/{id}")]
        public virtual async Task<ActionResult<T>> GetOneFullAsync(string id)
        {
            try
            {
                EnsureFromAllowed();

                var result = await _apiLogic.GetOneFullAsync(id);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetOneSimpleAsync + "/{id}")]
        public virtual async Task<ActionResult<T>> GetOneSimpleAsync(string id)
        {
            try
            {
                EnsureFromAllowed();

                var result = await _apiLogic.GetOneSimpleAsync(id);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpPut(Route.UpdateAsync)]
        public virtual async Task<ActionResult<T>> UpdateAsync(T entityUpdate)
        {
            try
            {
                EnsureFromAllowed();

                var result = await _apiLogic.UpdateAsync(entityUpdate);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpPut(Route.UpdatePropertyAsync + "/{id}")]
        public async Task<ActionResult<T>> UpdatePropertyAsync(string id, Dictionary<string, object> propertyValueDictionary)
        {
            try
            {
                EnsureFromAllowed();

                var result = await _apiLogic.UpdatePropertyAsync(id, propertyValueDictionary);
                return result;
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }

        [HttpDelete(Route.DeleteOneAsync + "/{id}")]
        public virtual async Task<ActionResult> DeleteOneAsync(string id)
        {
            try
            {
                EnsureFromAllowed();

                await _apiLogic.DeleteOneAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return CatchExceptions(e);
            }
        }
    }
}
