using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Api.Processes.Process_1
{
    public enum MyCustomProcessStep
    {
        [BsonRepresentation(BsonType.String)]
        Step0 = 0,

        [BsonRepresentation(BsonType.String)]
        Step1 = 1,

        [BsonRepresentation(BsonType.String)]
        Step2 = 2,

        [BsonRepresentation(BsonType.String)]
        Step3 = 3,

        [BsonRepresentation(BsonType.String)]
        StepEnd = 4,
    }
}
