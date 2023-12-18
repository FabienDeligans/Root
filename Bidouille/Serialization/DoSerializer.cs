using System.Text.Json;

namespace Bidouille.Serialization
{
    public class DoSerializer
    {
        public (bool, double, double) Do(int nb)
        {
            var objectToSerialize = new ObjectToSerialize
            {
                Nom = $"Deligans", 
                Prenom = $"Fabien"
            };

            var start = DateTime.Now;
            for (var i = 0; i < nb; i++)
            {
                var objectSerialized = JsonSerializer.Serialize(objectToSerialize); 
            }
            var stop = DateTime.Now;
            var duration = (stop - start).TotalMilliseconds;
            Console.WriteLine($"Classic serialization      : {duration} ms");

            start = DateTime.Now;
            for (var i = 0; i < nb; i++)
            {
                var objectSerialized = JsonSerializer.Serialize(objectToSerialize, SerializerContext.Default.ObjectToSerialize);
            }
            stop = DateTime.Now;
            var durationNew = (stop - start).TotalMilliseconds;

            var result = durationNew > duration ? "!!!" : null; 
            Console.WriteLine($"With context serialization : {durationNew} ms {result}");

            Console.WriteLine($"");

            return (string.IsNullOrEmpty(result), duration, durationNew);
        }
    }
}
