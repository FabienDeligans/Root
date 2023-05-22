using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Library.Models.Process
{
    public enum ProcessType
    {
        [BsonRepresentation(BsonType.String)]
        MonProcess01 = 1
    }
}
