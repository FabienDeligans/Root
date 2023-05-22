using Api.Logics;
using Api.Processes.Process_1;
using Library.Api.ApiControllerProvider;
using Library.Api.ApiExceptionManager;
using Library.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamilyController : BaseApiController<Family>
    {
        private readonly FamilyLogic _familyLogic;
        private readonly ClientProcess_1 _clientProcess_1;
        public FamilyController(
            FamilyLogic familyLogic, 
            ApiExceptionManager apiExceptionManager, 
            ClientProcess_1 process) 
            : base(familyLogic, apiExceptionManager)
        {
            _clientProcess_1 = process;
            _familyLogic = familyLogic;
        }

        // TODO Test Process
        public override Task<ActionResult<Family>> CreateAsync(Family entity)
        {
            _clientProcess_1.RunProcess(entity);
            return base.CreateAsync(entity);
        }
    }
}
