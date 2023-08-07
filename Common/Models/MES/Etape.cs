using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class Etape : Entity
{
    public int? NumeroEtape { get; set; } 
    
    public string Nom { get; set; }

    public string? Commentaire { get; set; }

    public List<ArticleConsome>? ArticlesConsommes { get; set; }

    public TimeSpan DureePrevue { get; set; }
    
    [BsonIgnore]
    public List<Gamme>? GammeFabrications { get; set;}
}

public class ArticleConsome
{
    public string ArticleId { get; set; }
    public string ArticleNom { get; set; }
    public int QuantityToUse { get; set; }
    public int QuantityUsed { get; set; }

    public DateTime? DateUsed { get; set; }
}