using Common.Models.CustomAttribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class Ope : Entity
{
    public int Step { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public bool Done { get; set; }

    public Dictionary<string, int> ArticlesUsedIdQuantity { get; set; }

    [ForeignKey(typeof(Article))]
    public string ArticleId { get; set; }
        
    [BsonIgnore]
    public Article? Article { get; set; }

}