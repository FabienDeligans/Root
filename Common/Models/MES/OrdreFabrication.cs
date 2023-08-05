using Common.Models.CustomAttribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class OrdreFabrication : Entity
{
    [ForeignKey(typeof(Article))]
    public string? ArticleId { get; set; }

    [BsonIgnore]
    public Article? ArticleFabrique { get; set; }

    [ForeignKey(typeof(Gamme))]
    public string GammeId { get; set; }

    [BsonIgnore]
    public Gamme? Gamme { get; set; }
    public DateTime? DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public List<Etape>? EtapesExecuted { get; set; }
}