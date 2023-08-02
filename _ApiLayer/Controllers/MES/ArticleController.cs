using _ApiLayer.Controllers.ApiControllerBase;
using _LogicLayer.Logics.LogicBase;
using Common.Models.MES;
using Microsoft.AspNetCore.Mvc;

namespace _ApiLayer.Controllers.MES
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : BaseApiController<Article>
    {
        private readonly ILogic<Article> _articleLogic;
        public ArticleController(
            ILogic<Article> apiLogic,
            ApiExceptionManager.ApiExceptionManager apiExceptionManager)
            : base(apiLogic, apiExceptionManager)
        {
            _articleLogic = apiLogic;
        }
    }
}
