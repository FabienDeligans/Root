using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics.LogicBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers.MES;

[ApiController]
[Route("[controller]")]
public class OfController : BaseApiController<Of>
{
    private readonly ILogic<Of> _ofLogic;
    public OfController(
        ILogic<Of> apiLogic,
        ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        : base(apiLogic, apiExceptionManager)
    {
        _ofLogic = apiLogic;
    }
}