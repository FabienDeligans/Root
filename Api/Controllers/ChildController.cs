using Api.Logics;
using Library.Api.ApiControllerProvider;
using Library.Api.ApiExceptionManager;
using Library.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildController : BaseApiController<Child>
    {
        private readonly ChildLogic _childLogic;
        public ChildController(ChildLogic apiLogic, ApiExceptionManager apiExceptionManager) : base(apiLogic, apiExceptionManager)
        {
            _childLogic = apiLogic;
        }
    }
}
