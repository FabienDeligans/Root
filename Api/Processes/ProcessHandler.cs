using Api.Processes.Process1;
using Library.Processes.Models;

namespace Api.Processes
{
    public class ProcessHandler : IObservable<Process>
    {
        private List<Process> _processes;
        private List<IObserver<Process>> _observers;

        public ProcessHandler(ClientProcess1 clientProcess1)
        {
            _processes = new List<Process>();
            _observers = new List<IObserver<Process>>();

            clientProcess1.Subscribe(this);
        }

        public IDisposable Subscribe(IObserver<Process> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                foreach (var process in _processes)
                {
                    observer.OnNext(process);
                }
            }
            return new DisposableSubscription<Process>(_observers, observer);
        }
    }
}
