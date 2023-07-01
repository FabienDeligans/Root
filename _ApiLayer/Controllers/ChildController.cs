using _ApiLayer.ApiControllerProvider;
using _LogicLayer.Logics;
using Common.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildController : BaseApiController<Child>
    {
        private readonly ILogic<Child> _childLogic;
        public ChildController(
            ILogic<Child> apiLogic, 
            ApiExceptionManager.ApiExceptionManager apiExceptionManager) 
            : base(apiLogic, apiExceptionManager)
        {
            _childLogic = apiLogic;
        }
    }
}
