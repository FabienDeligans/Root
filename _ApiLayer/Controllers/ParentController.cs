using _LogicLayer.Logics;
using Back._Api.ApiControllerProvider;
using Back._Api.ApiExceptionManager;
using Common.Models.Business;
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
