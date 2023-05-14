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
    public class ParentController : BaseApiController<Parent>
    {
        private readonly ParentLogic _parentLogic;
        public ParentController(IOptions<SettingsApi> options, ParentLogic parentLogic) : base(options, parentLogic)
        {
            _parentLogic = parentLogic;
        }
    }
}
