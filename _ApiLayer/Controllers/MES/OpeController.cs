using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics.LogicBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers.MES;

[ApiController]
[Route("[controller]")]
public class OpeController : BaseApiController<Ope>
{
    private readonly ILogic<Ope> _opeLogic;
    public OpeController(
        ILogic<Ope> apiLogic,
        ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        : base(apiLogic, apiExceptionManager)
    {
        _opeLogic = apiLogic;
    }
}