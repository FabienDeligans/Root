using _LogicLayer.Logics;
using Library._Api.ApiControllerProvider;
using Library._Api.ApiExceptionManager;
using Library._LogicLayer.Logic;
using Library._Providers.Models.Business;
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
