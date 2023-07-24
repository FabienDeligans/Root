using Common.Models.CustomAttribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES.Article;

public class ManufacturedArticle : Entity, IArticle
{
    public ManufacturedArticle()
    {
        TypeArticle = new TypeArticle();
    }

    public string Name { get; set; }
    public double Quantity { get; set; }
    public TypeArticle TypeArticle { get; set; }

    public string Version { get; set; }

    [ForeignKey(typeof(Of))]
    public string? OfId { get; set; }

    [BsonIgnore]
    public Of? Of { get; set; }
}