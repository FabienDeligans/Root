using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics.LogicBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers.MES;

[ApiController]
[Route("[controller]")]
public class EtapeController : BaseApiController<Etape>
{
    private readonly ILogic<Etape> _etapeLogic;
    public EtapeController(
        ILogic<Etape> apiLogic,
        ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        : base(apiLogic, apiExceptionManager)
    {
        _etapeLogic = apiLogic;
    }
}