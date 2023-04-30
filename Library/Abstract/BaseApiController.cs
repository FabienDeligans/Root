using Library.Interfaces;
using Library.Models;
using Library.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Library.Abstract
{
    [ApiController]
    public abstract class BaseApiController<T> : ControllerBase, IApiController<T> where T : IEntity
    {
        protected readonly IApiLogic<T> _apiLogic;

        public BaseApiController(IApiLogic<T> apiLogic)
        {
            _apiLogic = apiLogic;
        }

        [HttpDelete(Route.DropCollectionAsync)]
        public virtual async Task<ActionResult> DropCollectionAsync()
        {
            await _apiLogic.DropCollectionAsync();
            return Ok();
        }

        [HttpGet(Route.CountDataAsync)]
        public virtual async Task<long> CountDataAsync()
        {
            return await _apiLogic.CountDataAsync();
        }

        [HttpPost(Route.CreateAsync)]
        public virtual async Task<ActionResult<T>> CreateAsync(T entity)
        {
            var result = await _apiLogic.CreateAsync(entity);
            return new ActionResult<T>(result);
        }

        [HttpPost(Route.CreateManyAsync)]
        public virtual async Task<ActionResult<IEnumerable<T>>> CreateManyAsync(IEnumerable<T> entities)
        {
            var result = await _apiLogic.CreateManyAsync(entities);
            return new ActionResult<IEnumerable<T>>(result);
        }

        [HttpGet(Route.GetAllAsync)]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
        {
            var result = await _apiLogic.GetAllAsync();
            return new ActionResult<IEnumerable<T>>(result);
        }

        [HttpGet(Route.GetOneFullAsync + "/{id}")]
        public virtual async Task<ActionResult<T>> GetOneFullAsync(string id)
        {
            var result = await _apiLogic.GetOneFullAsync(id);
            return new ActionResult<T>(result);
        }

        [HttpGet( Route.GetOneSimpleAsync + "/{id}")]
        public virtual async Task<ActionResult<T>> GetOneSimpleAsync(string id)
        {
            var result = await _apiLogic.GetOneSimpleAsync(id);
            return new ActionResult<T>(result);
        }

        [HttpPut(Route.UpdateAsync)]
        public virtual async Task<ActionResult<T>> UpdateAsync(T entityUpdate)
        {
            var result = await _apiLogic.UpdateAsync(entityUpdate);
            return new ActionResult<T>(result);
        }

        [HttpPut(Route.UpdatePropertyAsync + "/{id}")]
        public async Task<ActionResult<T>> UpdatePropertyAsync(string id, Dictionary<string, string> propertyValueDictionary)
        {
            var result = await _apiLogic.UpdatePropertyAsync(id, propertyValueDictionary);
            return result; 
        }

        [HttpDelete(Route.DeleteOneAsync + "/{id}")]
        public virtual async Task<ActionResult> DeleteOneAsync(string id)
        {
            await _apiLogic.DeleteOneAsync(id);
            return Ok();
        }
    }
}
