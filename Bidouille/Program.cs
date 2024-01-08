using Bidouille.Chain_Of_Responsability.Processes.Process1;
using Bidouille.Chain_Of_Responsability;
using Bidouille.Decorator;
using Bidouille.DelegateUsage;
using Bidouille.Event_Pattern;
using Bidouille.Observer_Pattern;
using Bidouille.Serialization;
using Common.Helper;

namespace Bidouille
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            #region Decorator

            var truc = new ClassToBeDecorated();
            var decA = new DecoratorA(truc);
            var decB = new DecoratorB(decA);

            Console.WriteLine(truc.Do());
            Console.WriteLine(decA.Do());
            Console.WriteLine(decB.Do());

            #endregion

            #region Observer

            var handler = new ConcreteHandler();
            var observer1 = new ConcreteObserver();
            var observer2 = new ConcreteObserver();

            var testDelegate = new TestDelegate(); 
            testDelegate.Do();
            
            var result0 = DuStringAsync(20).ConfigureAwait(false); 
            var result00 = DuStringAsync(20).GetAwaiter().GetResult(); 
            var result1 = DoString(20);




            var provider = new TrucHandler<int>();
            var observer1 = new TrucObserver<int>();
            var observer2 = new TrucObserver<int>();

            handler.Do(1);
            observer1.Subscribe(handler);
            observer2.Subscribe(handler);

            for (var i = 2; i < 5; i++)
            {
                handler.Do(i);
            }
            observer1.UnSubscribe();
            handler.Do(10);
            handler.End();

            #endregion

            #region Events

            var sender = new Sender();
            var listner1 = new Listner1(sender);
            var listner2 = new Listner2(sender);
            sender.Do();

            #endregion

            #region Chain of responsability

            var client = new Client();
            client.ClientProcess(new StartProcess());

            #endregion

            #region Serialize

            var listCount = new List<bool>(); 
            var durations = new List<double>(); 
            var durationNews = new List<double>();
            var dif = new List<double>();
            for (var i = 0; i < 1_000; i++)
            {
                var doSerialize = new DoSerializer();
                var (count, duration, durationNew) = doSerialize.Do(1_000);
                listCount.Add(count);
                durations.Add(duration);
                durationNews.Add(durationNew);
                dif.Add(duration - durationNew);
            }

            Console.WriteLine($"Nb classic serialization      : " + listCount.Count(v => v)); 
            Console.WriteLine($"Nb Serialization with context : " + listCount.Count(v => !v));
            
            Console.WriteLine($"");

            Console.WriteLine($"Duration Classic serialization     : " + durations.Average());
            Console.WriteLine($"Duration Classic serialization Min : " + durations.Min());
            Console.WriteLine($"Duration Classic serialization Max : " + durations.Max());
            
            Console.WriteLine($"");

            Console.WriteLine($"Duration Context serialization        : " + durationNews.Average());
            Console.WriteLine($"DurationNew Context serialization Min : " + durationNews.Min());
            Console.WriteLine($"DurationNew Context serialization Max : " + durationNews.Max());
            var client = new Client();
            client.ClientProcess(new Do1Process());
        }

            Console.WriteLine($"");
            Console.WriteLine($"Difference average : {dif.Average()}");
            Console.WriteLine($"Difference min     : {dif.Min()}");
            Console.WriteLine($"Difference max     : {dif.Max()}");

        public static async Task<string> DuStringAsync(int nb)
        {
            return await Task.Run(()=> RandomHelper.GetRandomString(nb)); 
        }

        public static string DoString(int nb)
        {
            return RandomHelper.GetRandomString(nb);
        }



    }
}