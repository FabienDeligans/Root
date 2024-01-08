namespace Bidouille.DelegateUsage
{
    public class TestDelegate
    {
        public void Do()
        {
            var handler = DelegateFunction;
            handler.Invoke($"test");
            // test

            Do2($"message", handler);
            // 2 message

            Do3($"truc", handler);
            // 3 truc

            var message1 = "message1";
            var message2 = "message2"; 
            handler += (_ =>  Method1(message1));
            handler += (_ =>  Method2(message2));

            handler.Invoke($"base message"); 
        }

        public void Method1(string message)
        {
            Console.WriteLine(message);
        }
        public void Method2(string message)
        {
            Console.WriteLine(message);
        }

        public void Do2(string message, Action<string> callback)
        {
            message = $"2 {message}";
            callback.Invoke(message);
        }

        public void Do3(string message, Action<string> callback)
        {
            message = $"3 {message}";
            callback.Invoke(message);
        }

        public void DelegateFunction(string message)
        {
            Console.WriteLine(message);
        }
    }
}
