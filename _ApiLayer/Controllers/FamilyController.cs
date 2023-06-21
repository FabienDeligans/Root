using Back._Api.ApiControllerProvider;
using Back._Api.ApiExceptionManager;
using Back._LogicLayer.Logic;
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
            ApiExceptionManager apiExceptionManager) 
            : base(familyLogic, apiExceptionManager)
        {
            _familyLogic = familyLogic;
        }
    }
}
