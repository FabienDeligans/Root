using Common.Models.CustomAttribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class GammeEtape : Entity
{
    [ForeignKey(typeof(Gamme))]
    public string GammeId { get; set; }

    [BsonIgnore]
    public Gamme? Gamme { get; set; }

    [ForeignKey(typeof(Etape))]
    public string EtapeId { get; set; }

    [BsonIgnore]
    public Etape? Etape { get; set; }

}