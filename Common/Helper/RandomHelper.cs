namespace Common.Helper
{
    public class RandomHelper
    {
        public static string GetRandomString(int length)
        {
            var randomChar = new char[length];

            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            for (var i = 0; i < length; i++)
            {
                randomChar[i] = characters[new Random().Next(characters.Length)]; 
            }

            return new string(randomChar); 
        }

        private static Random _random = new Random(10);
        public static bool UseRandom = true;
        public static T GetRandomEnum<T>() where T : Enum
        {
            var rand = UseRandom ? new Random() : _random;
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(rand.Next(values.Length))!;
        }
    }
}
