using _LogicLayer.Logics.LogicBase;
using _Providers.DatabaseProviders;
using Common.Models.MES;

namespace _LogicLayer.Logics.MES;

public class ManufacturedArticleLogic : BaseApiLogic<ManufacturedArticle>
{
    public ManufacturedArticleLogic(IApiServiceDatabase serviceDatabase)
        : base(serviceDatabase)
    {
    }
}

public class PurchasedArticleLogic : BaseApiLogic<PurchasedArticle>
{
    public PurchasedArticleLogic(IApiServiceDatabase serviceDatabase)
        : base(serviceDatabase)
    {
    }
}