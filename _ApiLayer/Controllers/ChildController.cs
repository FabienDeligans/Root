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
