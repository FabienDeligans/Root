using Api.Processes.Process_1;
using Library.Models.Process;

namespace Api.Processes
{
    public class HandlerManager
    {
        private readonly Handler _handler; 
        private readonly ClientProcess_1 _clientProcess1;

        public HandlerManager(Handler handler, ClientProcess_1 clientProcess1)
        {
            _handler = handler;
            _clientProcess1 = clientProcess1;

            _clientProcess1.SetHandler(_handler);
        }

        public void Do(ProcessModel process)
        {
            _handler.Handle(process);
        }
    }
}
