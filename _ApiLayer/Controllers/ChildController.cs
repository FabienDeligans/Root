using _LogicLayer.Logics;
using Back._Api.ApiControllerProvider;
using Back._Api.ApiExceptionManager;
using Common.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildController : BaseApiController<Child>
    {
        private readonly ChildLogic _childLogic;
        public ChildController(
            ChildLogic apiLogic, 
            ApiExceptionManager apiExceptionManager) 
            : base(apiLogic, apiExceptionManager)
        {
            _childLogic = apiLogic;
        }
    }
}
