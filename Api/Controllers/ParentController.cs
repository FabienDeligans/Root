using Api.Logics;
using Library.Api.ApiControllerProvider;
using Library.Api.ApiExceptionManager;
using Library.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParentController : BaseApiController<Parent>
    {
        private readonly ParentLogic _parentLogic;
        public ParentController(ParentLogic parentLogic, ApiExceptionManager apiExceptionManager) : base(parentLogic, apiExceptionManager)
        {
            _parentLogic = parentLogic;
        }
    }
}
