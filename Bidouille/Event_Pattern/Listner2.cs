namespace Bidouille.Event_Pattern
{
    public class Listner2
    {
        private Sender Sender { get; set; }

        public Listner2(Sender sender)
        {
            Sender = sender;
            Sender.SendEvent += OnSendEvent;
        }

        private void OnSendEvent(object? sender, ICustomEvent e)
        {
            // Do something

            var myCustomEvent = (MyCustomEvent) e; 
            Console.WriteLine(myCustomEvent.ValueEvent);
        }

    }
}