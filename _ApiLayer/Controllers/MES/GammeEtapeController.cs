using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics.LogicBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers.MES;

[ApiController]
[Route("[controller]")]
public class GammeEtapeController : BaseApiController<GammeEtape>
{
    private readonly ILogic<GammeEtape> _gammeEtapeLogic;
    public GammeEtapeController(
        ILogic<GammeEtape> apiLogic,
        ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        : base(apiLogic, apiExceptionManager)
    {
        _gammeEtapeLogic = apiLogic;
    }
}