namespace Bidouille.Event_Pattern
{
    public class Sender
    {
        public event EventHandler<EventArgs>? SendEvent; 

        public void Do()
        {
            // Do something

            var eventToSend = new MyCustomEvent()
            {
                ValueEvent = "aze-123"
            }; 

            CallEvent(eventToSend); 
        }

        private void CallEvent(EventArgs eventToSend)
        {
            SendEvent?.Invoke(this, eventToSend);
        }
    }
}
