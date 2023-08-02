using Common.Models.CustomAttribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class Gamme : Entity
{
    [ForeignKey(typeof(Article))]
    public string ArticleId { get; set; }
        
    [BsonIgnore]
    public Article? Article { get; set; }
        
    public string Nom { get; set; }

    public int Quantity { get; set; }

    [BsonIgnore]
    public List<Etape>? Etapes { get; set; }
}