using Api.Logics;
using Library.Api.ApiControllerProvider;
using Library.Models.Business;
using Library.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildController : BaseApiController<Child>
    {
        private readonly ChildLogic _childLogic; 
        public ChildController(IOptions<SettingsApi> options, ChildLogic apiLogic) : base(options, apiLogic)
        {
            _childLogic = apiLogic;
        }
    }
}
