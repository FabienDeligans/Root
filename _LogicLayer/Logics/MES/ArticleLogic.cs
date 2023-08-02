using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES
{
    public class ArticleLogic : BaseApiLogic<Article>
    {
        public ArticleLogic(IApiServiceDatabase serviceDatabase)
            : base(serviceDatabase)
        {
        }
    }
}
