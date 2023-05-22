using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Library.Models.Process
{
    public enum ProcessState
    {
        [BsonRepresentation(BsonType.String)]
        Processing = 0,
        
        [BsonRepresentation(BsonType.String)]
        Success = 1,
        
        [BsonRepresentation(BsonType.String)] 
        Failure = 2,
    }
}
