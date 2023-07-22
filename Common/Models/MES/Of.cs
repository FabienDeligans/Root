using Common.Models.CustomAttribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES
{
    public class Of : Entity
    {
        [ForeignKey(typeof(Article))]
        public string ArticleId { get; set; }

        [BsonIgnore]
        public Article? Article { get; set; }
    }
}
