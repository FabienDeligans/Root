using Api.Processes.Process_1;
using Library.Events;
using Library.Models.Process;
using Library.Process;

namespace Api.Processes
{
    public class Handler
    {
        public event EventHandler<ICustomEvent>? SendEvent;

        public void Handle(ProcessModel process)
        {
            var value = process.ProcessType;

            var eventToSend = new MyCustomEvent()
            {
                ValueEvent = value.ToString(),
            };

            CallEvent(eventToSend);
        }

        private void CallEvent(ICustomEvent eventToSend)
        {
            SendEvent?.Invoke(this, eventToSend);
        }

    }
}
