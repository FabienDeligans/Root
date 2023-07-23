using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics.LogicBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers.MES;

[ApiController]
[Route("[controller]")]
public class GammeController : BaseApiController<Gamme>
{
    private readonly ILogic<Gamme> _gammeLogic;
    public GammeController(
        ILogic<Gamme> apiLogic,
        ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        : base(apiLogic, apiExceptionManager)
    {
        _gammeLogic = apiLogic;
    }
}