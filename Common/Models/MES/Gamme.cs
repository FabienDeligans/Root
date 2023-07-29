using Common.Models.CustomAttribute;
using Common.Models.MES.Article;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class Gamme : Entity
{
    [ForeignKey(typeof(ManufacturedArticle))]
    public string ManufacturedArticleId { get; set; }

    [BsonIgnore]
    public ManufacturedArticle? ManufacturedArticle { get; set; }

    public List<Ope> OpeGamme { get; set; }
}