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
    public class FamilyController : BaseApiController<Family>
    {
        private readonly FamilyLogic _familyLogic;
        public FamilyController(IOptions<SettingsApi> options, FamilyLogic familyLogic) : base(options, familyLogic)
        {
            _familyLogic = familyLogic;
        }
    }
}
