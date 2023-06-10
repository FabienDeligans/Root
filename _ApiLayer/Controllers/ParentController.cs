using _LogicLayer.Logics;
using Library._Api.ApiControllerProvider;
using Library._Api.ApiExceptionManager;
using Library._Providers.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParentController : BaseApiController<Parent>
    {
        private readonly ParentLogic _parentLogic;
        public ParentController(
            ParentLogic parentLogic, 
            ApiExceptionManager apiExceptionManager) 
            : base(parentLogic, apiExceptionManager)
        {
            _parentLogic = parentLogic;
        }
    }
}
