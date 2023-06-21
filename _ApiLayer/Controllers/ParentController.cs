using Back._Api.ApiControllerProvider;
using Back._Api.ApiExceptionManager;
using Back._LogicLayer.Logic;
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
            ApiExceptionManager apiExceptionManager) 
            : base(parentLogic, apiExceptionManager)
        {
            _parentLogic = parentLogic;
        }
    }
}
