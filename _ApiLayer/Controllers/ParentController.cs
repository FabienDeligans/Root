using _ApiLayer.ApiControllerProvider;
using _LogicLayer.Logics;
using Common.Models.Business;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParentController : BaseApiController<Parent>
    {
        private readonly ILogic<Parent> _parentLogic;
        public ParentController(
            ILogic<Parent> parentLogic, 
            ApiExceptionManager.ApiExceptionManager apiExceptionManager) 
            : base(parentLogic, apiExceptionManager)
        {
            _parentLogic = parentLogic;
        }
    }
}
