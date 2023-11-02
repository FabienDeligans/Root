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

        public static T GetRandomEnum<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(new Random().Next(values.Length))!; 
        }
    }
}
