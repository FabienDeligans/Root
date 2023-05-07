using Api.Logics;
using Library.Api.ApiControllerProvider;
using Library.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildController : BaseApiController<Child>
    {
        private readonly ChildLogic _childLogic; 
        public ChildController(ChildLogic apiLogic) : base(apiLogic)
        {
            _childLogic = apiLogic;
        }
    }
}
