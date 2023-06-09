using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Library._Providers.Models.Business
{
    public class Family : Entity
    {
        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string? Name { get; set; }

        [BsonIgnore]
        public IEnumerable<Parent>? Parents { get; set; }

        [BsonIgnore]
        public IEnumerable<Child>? Children { get; set; }
    }
}
