using Bidouille.Chain_Of_Responsability.Processes.Process1;
using Bidouille.Chain_Of_Responsability;
using Bidouille.Decorator;
using Bidouille.Event_Pattern;
using Bidouille.Observer_Pattern;

namespace Bidouille
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Decorator
            
            var truc = new ClassToBeDecorated();
            var decA = new DecoratorA(truc);
            var decB = new DecoratorB(decA);

            Console.WriteLine(decB.Do());

            #endregion

            #region Observer

            var handler = new TrucHandler<int>();
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
        }
    }
}