using Common.Models.CustomAttribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public abstract class Article : Entity
{
    public string Name { get; set; }

    public double Quantity { get; set; }

    public TypeArticle TypeArticle { get; set; }

}

public class ManufacturedArticle : Article
{
    public ManufacturedArticle()
    {
        TypeArticle = new TypeArticle();
    }

    public string? Version { get; set; }

    [ForeignKey(typeof(Of))]
    public string? OfId { get; set; }

    [BsonIgnore]
    public Of? Of { get; set; }

    [ForeignKey(typeof(Gamme))]
    public string? GammeId { get; set; }

    [BsonIgnore]
    public Gamme? Gamme { get; set; }
}

public class PurchasedArticle : Article
{
    public PurchasedArticle()
    {
        TypeArticle = TypeArticle.Purchased; 
    }
}