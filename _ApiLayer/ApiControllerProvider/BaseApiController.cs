using _LogicLayer.Logics;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Route = Common.Route;

namespace _ApiLayer.ApiControllerProvider
{
    [ApiController]
    public abstract class BaseApiController<T> : ControllerBase, IApiController<T> where T : IEntity
    {
        protected readonly ILogic<T> _apiLogic;
        protected readonly ApiExceptionManager.ApiExceptionManager _apiExceptionManager; 

        public BaseApiController(ILogic<T> apiLogic, ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        {
            _apiLogic = apiLogic;
            _apiExceptionManager = apiExceptionManager;
        }
        
        [HttpDelete(Route.DropCollectionAsync)]
        public virtual async Task<ActionResult> DropCollectionAsync()
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                await _apiLogic.DropCollectionAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.CountDataAsync)]
        public virtual async Task<ActionResult<long>> CountDataAsync()
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                return await _apiLogic.CountDataAsync();
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpPost(Route.CreateAsync)]
        public virtual async Task<ActionResult<T>> CreateAsync(T entity)
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.CreateAsync(entity);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpPost(Route.CreateManyAsync)]
        public virtual async Task<ActionResult<IEnumerable<T>>> CreateManyAsync(IEnumerable<T> entities)
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.CreateManyAsync(entities);
                return new ActionResult<IEnumerable<T>>(result);
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetAllAsync)]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.GetAllAsync();
                return new ActionResult<IEnumerable<T>>(result);
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetAllFilteredByPropertyEqualAsync + "/{propertyName}/{value}")]
        public async Task<ActionResult<IEnumerable<T>?>> GetAllFilteredByPropertyEqualAsync(string propertyName, string value)
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.GetAllFilteredByPropertyEqualAsync(propertyName, value);
                return new ActionResult<IEnumerable<T>?>(result);
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetOneFullAsync + "/{id}")]
        public virtual async Task<ActionResult<T>> GetOneFullAsync(string id)
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.GetOneFullAsync(id);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetOneSimpleAsync + "/{id}")]
        public virtual async Task<ActionResult<T>> GetOneSimpleAsync(string id)
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.GetOneSimpleAsync(id);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpPut(Route.UpdateAsync)]
        public virtual async Task<ActionResult<T>> UpdateAsync(T entityUpdate)
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.UpdateAsync(entityUpdate);
                return new ActionResult<T>(result);
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpPut(Route.UpdatePropertyAsync + "/{id}")]
        public async Task<ActionResult<T>> UpdatePropertyAsync(string id, Dictionary<string, object> propertyValueDictionary)
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                var result = await _apiLogic.UpdatePropertyAsync(id, propertyValueDictionary);
                return result;
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpDelete(Route.DeleteOneAsync + "/{id}")]
        public virtual async Task<ActionResult> DeleteOneAsync(string id)
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                await _apiLogic.DeleteOneAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }
    }
}
