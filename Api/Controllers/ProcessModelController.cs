using Api.Logics;
using Library.Api.ApiExceptionManager;
using Library.Models.Process;
using Microsoft.AspNetCore.Mvc;
using Route = Library.Settings.Route;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessModelController : ControllerBase
    {
        private readonly ProcessLogic _processLogic;
        private readonly ApiExceptionManager _apiExceptionManager;

        public ProcessModelController(ProcessLogic processLogic, ApiExceptionManager apiExceptionManager)
        {
            _processLogic = processLogic;
            _apiExceptionManager = apiExceptionManager;
        }

        [HttpDelete(Route.DropCollectionAsync)]
        public async Task<ActionResult> DropCollectionAsync()
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                await _processLogic.DropCollectionAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.CountDataAsync)]
        public async Task<ActionResult<long>> CountDataAsync()
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                return await _processLogic.CountDataAsync();
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        [HttpGet(Route.GetAllAsync)]
        public async Task<ActionResult<IEnumerable<ProcessModel>>> GetAllAsync()
        {
            try
            {
                _apiExceptionManager.EnsureFromAllowed(Request);

                var result = await _processLogic.GetAllAsync();
                return new ActionResult<IEnumerable<ProcessModel>?>(result);
            }
            catch (Exception e)
            {
                return _apiExceptionManager.CatchExceptions(e);
            }
        }

        //[HttpPost("RunProcess")]
        //public async Task<ActionResult> RunProcess(ProcessType processType)
        //{
        //    _processLogic.RunProcess(processType);
        //    return Ok();
        //}
    }
}
