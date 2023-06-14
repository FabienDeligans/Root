using _LogicLayer.Logics;
using Back._Api.ApiControllerProvider;
using Back._Api.ApiExceptionManager;
using Common.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamilyController : BaseApiController<Family>
    {
        private readonly FamilyLogic _familyLogic;
        public FamilyController(
            FamilyLogic familyLogic, 
            ApiExceptionManager apiExceptionManager) 
            : base(familyLogic, apiExceptionManager)
        {
            _familyLogic = familyLogic;
        }
    }
}
