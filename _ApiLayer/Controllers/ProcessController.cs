using _LogicLayer.Processes;
using Library._Api.ApiExceptionManager;
using Library._LogicLayer.Processes.Models;
using Microsoft.AspNetCore.Mvc;
using Route = Library.Settings.Route;

namespace _ApiLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcessController : ControllerBase
{
    private readonly ApiExceptionManager _apiExceptionManager;
    private readonly ProcessHandler _processHandler; 
    public ProcessController(
        ApiExceptionManager apiExceptionManager, 
        ProcessHandler processHandler)
    {
        _apiExceptionManager = apiExceptionManager;
        _processHandler = processHandler;
    }

    [HttpGet(Route.GetAllAsync)]
    public async Task<ActionResult<IEnumerable<Process>>> GetAllAsync()
    {
        try
        {
            _apiExceptionManager.EnsureFromAllowed(Request);

            var result = await _processHandler.GetAllAsync();
            return new ActionResult<IEnumerable<Process>>(result);
        }
        catch (Exception e)
        {
            return _apiExceptionManager.CatchExceptions(e);
        }
    }

    [HttpPost(Route.CreateSpecificProcess)]
    public async Task<ActionResult> CreateSpecificProcess(Process process)
    {
        try
        {
            _apiExceptionManager.EnsureFromAllowed(Request);

            _processHandler.CreateSpecificProcess(process);
            return Ok();
        }
        catch (Exception e)
        {
            return _apiExceptionManager.CatchExceptions(e);
        }
    }

    [HttpPost(Route.RunSpecificProcess)]
    public async Task<ActionResult> RunSpecificProcess(Process process)
    {
        try
        {
            _apiExceptionManager.EnsureFromAllowed(Request);

            _processHandler.RunSpecificProcess(process);
            return Ok();
        }
        catch (Exception e)
        {
            return _apiExceptionManager.CatchExceptions(e);
        }
    }

    [HttpPost(Route.RunAllFaillureProcesses)]
    public async Task<ActionResult> RunAllFaillureProcesses(Process process)
    {
        try
        {
            _apiExceptionManager.EnsureFromAllowed(Request);

            _processHandler.RunAllFaillureProcesses();
            return Ok();
        }
        catch (Exception e)
        {
            return _apiExceptionManager.CatchExceptions(e);
        }
    }

    [HttpDelete(Route.DropCollectionAsync)]
    public async Task<ActionResult> DropCollectionAsync()
    {
        try
        {
            _apiExceptionManager.EnsureFromAllowed(Request);

            await _processHandler.DropCollectionAsync();
            return Ok();
        }
        catch (Exception e)
        {
            return _apiExceptionManager.CatchExceptions(e);
        }
    }
}