using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Library._Providers.Models.Business
{
    public class Activity : Entity
    {
        [Required]
        public string ActivityNom { get; set; }

        [BsonIgnore]
        public IEnumerable<Inscription>? Inscriptions { get; set; }

        [BsonIgnore]
        public List<GroupDetail>? Groupes { get; set; }
    }
}
