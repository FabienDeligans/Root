using System.Text.Json.Serialization;

namespace Bidouille.Serialization
{
    public class ObjectToSerialize
    { 
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }

    [JsonSerializable(typeof(ObjectToSerialize))]
    public partial class SerializerContext : JsonSerializerContext
    {
    }
}
