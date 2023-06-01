using Api.Logics;
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
        public FamilyController(FamilyLogic familyLogic, ApiExceptionManager apiExceptionManager) : base(familyLogic, apiExceptionManager)
        {
            _familyLogic = familyLogic;
        }
    }
}
