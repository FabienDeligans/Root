using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics.LogicBase;
using Common.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamilyController : BaseApiController<Family>
    {
        private readonly ILogic<Family> _familyLogic;
        public FamilyController(
            ILogic<Family> familyLogic, 
            ApiExceptionManager.ApiExceptionManager apiExceptionManager) 
            : base(familyLogic, apiExceptionManager)
        {
            _familyLogic = familyLogic;
        }
    }
}
