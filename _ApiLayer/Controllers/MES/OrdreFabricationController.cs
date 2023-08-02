using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics.LogicBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers.MES;

[ApiController]
[Route("[controller]")]
public class OrdreFabricationController : BaseApiController<OrdreFabrication>
{
    private readonly ILogic<OrdreFabrication> _ordreFabricationLogic;
    public OrdreFabricationController(
        ILogic<OrdreFabrication> apiLogic,
        ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        : base(apiLogic, apiExceptionManager)
    {
        _ordreFabricationLogic = apiLogic;
    }
}