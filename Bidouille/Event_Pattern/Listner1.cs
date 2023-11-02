namespace Bidouille.Event_Pattern
{
    public class Listner1
    {
        private Sender Sender { get; set; }

        public Listner1(Sender sender)
        {
            Sender = sender;
            Sender.SendEvent += OnSendEvent; 
        }

        private void OnSendEvent(object? sender, ICustomEvent e)
        {
            // Do something

            var myCustomEvent = (MyCustomEvent)e;
            Console.WriteLine(myCustomEvent.ValueEvent);
        }

    }
}
