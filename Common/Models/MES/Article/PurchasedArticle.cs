namespace Common.Models.MES.Article;

public class PurchasedArticle : Entity, IArticle
{
    public string Name { get; set; }
    public double Quantity { get; set; }

}