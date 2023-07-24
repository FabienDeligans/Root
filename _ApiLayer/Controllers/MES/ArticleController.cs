using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics.LogicBase;
using Common.Models.MES.Article;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers.MES;

[ApiController]
[Route("[controller]")]
public class ManufacturedArticleController : BaseApiController<ManufacturedArticle>
{
    private readonly ILogic<ManufacturedArticle> _manufacturedArticleLogic;
    public ManufacturedArticleController(
        ILogic<ManufacturedArticle> apiLogic,
        ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        : base(apiLogic, apiExceptionManager)
    {
        _manufacturedArticleLogic = apiLogic;
    }
}

[ApiController]
[Route("[controller]")]
public class PurchasedArticleController : BaseApiController<PurchasedArticle>
{
    private readonly ILogic<PurchasedArticle> _purchasedArticleLogic;
    public PurchasedArticleController(
        ILogic<PurchasedArticle> apiLogic,
        ApiExceptionManager.ApiExceptionManager apiExceptionManager)
        : base(apiLogic, apiExceptionManager)
    {
        _purchasedArticleLogic = apiLogic;
    }
}