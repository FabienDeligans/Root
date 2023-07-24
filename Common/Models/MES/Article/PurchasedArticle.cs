namespace Common.Models.MES.Article;

public class PurchasedArticle : Entity, IArticle
{
    public PurchasedArticle()
    {
        TypeArticle = TypeArticle.Purchased;
    }

    public string Name { get; set; }
    public double Quantity { get; set; }
    public TypeArticle TypeArticle { get; set; }

}