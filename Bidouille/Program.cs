using Bidouille.Chain_Of_Responsability.Processes.Process1;
using Bidouille.Chain_Of_Responsability;
using Bidouille.Event_Pattern;
using Bidouille.Observer_Pattern;

namespace Bidouille
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var provider = new TrucHandler<int>();
            var observer1 = new TrucObserver<int>();
            var observer2 = new TrucObserver<int>();

            provider.Do(1);
            observer1.Subscribe(provider);
            observer2.Subscribe(provider);

            for (var i = 2; i < 5; i++)
            {
                provider.Do(i);
            }
            observer1.UnSubscribe();
            provider.Do(10);
            provider.End();




            var sender = new Sender();
            var listner1 = new Listner1(sender);
            var listner2 = new Listner2(sender);

            sender.Do();





            var client = new Client();
            client.ClientProcess(new Do1Process());
        }
    }
}