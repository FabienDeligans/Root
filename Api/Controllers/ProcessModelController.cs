using Api.Logics;
using Library.Api.ApiControllerProvider;
using Library.Api.ApiExceptionManager;
using Library.Models.Process;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessModelController : BaseApiController<ProcessModel>
    {
        private readonly ProcessLogic _processLogic; 
        public ProcessModelController(ProcessLogic processLogic, ApiExceptionManager apiExceptionManager) : base(processLogic, apiExceptionManager)
        {
            _processLogic = processLogic;
        }
    }
}
