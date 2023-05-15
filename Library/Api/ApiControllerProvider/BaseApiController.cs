using Library.Api.ApiLogicProvider;
using Library.Models;
using Library.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.ApiControllerProvider
{
    [ApiController]
    public abstract class BaseApiController<T> : ControllerBase, IApiController<T> where T : IEntity
    {
        protected readonly IApiLogic<T> _apiLogic;
        protected readonly ApiExceptionManager.ApiExceptionManager ApiExceptionManager; 

        public BaseApiController(IApiLogic<T> apiLogic, ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        {
            _apiLogic = apiLogic;
            ApiExceptionManager = apiExceptionManager;
        }
        
        [HttpDelete(Route.DropCollectionAsync)]
        public virtual async Task<ActionResult> DropCollectionAsync()
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                await _apiLogic.DropCollectionAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.CountDataAsync)]
        public virtual async Task<ActionResult<long>> CountDataAsync()
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                return await _apiLogic.CountDataAsync();
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpPost(Route.CreateAsync)]
        public virtual async Task<ActionResult<T>> CreateAsync(T entity)
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.CreateAsync(entity);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpPost(Route.CreateManyAsync)]
        public virtual async Task<ActionResult<IEnumerable<T>>> CreateManyAsync(IEnumerable<T> entities)
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.CreateManyAsync(entities);
                return new ActionResult<IEnumerable<T>>(result);
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetAllAsync)]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.GetAllAsync();
                return new ActionResult<IEnumerable<T>>(result);
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetAllFilteredByPropertyEqualAsync + "/{propertyName}/{value}")]
        public async Task<ActionResult<IEnumerable<T>?>> GetAllFilteredByPropertyEqualAsync(string propertyName, string value)
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.GetAllFilteredByPropertyEqualAsync(propertyName, value);
                return new ActionResult<IEnumerable<T>?>(result);
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetOneFullAsync + "/{id}")]
        public virtual async Task<ActionResult<T>> GetOneFullAsync(string id)
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.GetOneFullAsync(id);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetOneSimpleAsync + "/{id}")]
        public virtual async Task<ActionResult<T>> GetOneSimpleAsync(string id)
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.GetOneSimpleAsync(id);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpPut(Route.UpdateAsync)]
        public virtual async Task<ActionResult<T>> UpdateAsync(T entityUpdate)
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.UpdateAsync(entityUpdate);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpPut(Route.UpdatePropertyAsync + "/{id}")]
        public async Task<ActionResult<T>> UpdatePropertyAsync(string id, Dictionary<string, object> propertyValueDictionary)
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.UpdatePropertyAsync(id, propertyValueDictionary);
                return result;
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpDelete(Route.DeleteOneAsync + "/{id}")]
        public virtual async Task<ActionResult> DeleteOneAsync(string id)
        {
            try
            {
                ApiExceptionManager.EnsureFromAllowed(Request);

                await _apiLogic.DeleteOneAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return ApiExceptionManager.CatchExceptions(e);
            }
        }
    }
}
