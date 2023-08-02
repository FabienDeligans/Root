using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES
{
    public class Article : Entity
    {
        public string Nom { get; set; }

        public bool EstFabrique { get; set; }

        [BsonIgnore]
        public List<Gamme>? GammesFabrication { get; set; }

        public int Stock { get; set; }
    }
}
