namespace Bidouille.Event_Pattern
{
    public class Sender
    {
        public event EventHandler<ICustomEvent>? SendEvent; 

        public void Do()
        {
            // Do something

            var eventToSend = new MyCustomEvent()
            {
                ValueEvent = "aze-123"
            }; 

            CallEvent(eventToSend); 
        }

        private void CallEvent(ICustomEvent eventToSend)
        {
            SendEvent?.Invoke(this, eventToSend);
        }
    }
}
