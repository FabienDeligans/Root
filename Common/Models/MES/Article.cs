using Common.Models.CustomAttribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class Article : Entity
{
    public string Name { get; set; }

    [ForeignKey(typeof(Of))]
    public string? OfId { get; set; }

    [BsonIgnore]
    public Of? Of { get; set; }

    public double Quantity { get; set; }

    public TypeArticle TypeArticle { get; set; }

    [BsonIgnore]
    public IEnumerable<Ope>? Operations { get; set; }
}