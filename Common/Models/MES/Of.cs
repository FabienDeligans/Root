using Common.Models.CustomAttribute;
using Common.Models.MES.Article;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES
{
    public class Of : Entity
    {
        [ForeignKey(typeof(Gamme))]
        public string? GammeId { get; set; }

        [BsonIgnore]
        public Gamme? Gamme { get; set; }

        [ForeignKey(typeof(ManufacturedArticle))]
        public string ManufacturedArticleId { get; set; }

        [BsonIgnore]
        public ManufacturedArticle? ManufacturedArticle { get; set; }
    }
}
