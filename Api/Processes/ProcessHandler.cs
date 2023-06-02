using Api.Logics;
using Api.Processes.Process1;
using Library.Processes.Models;

namespace Api.Processes
{
    public class ProcessHandler : IObservable<Process>
    {
        private List<Process> _processes;
        private List<IObserver<Process>> _observers;

        private readonly ProcessLogic _processLogic; 
        public ProcessHandler(
            ProcessLogic processLogic, 
            ClientProcess1 clientProcess1)
        {
            _processLogic = processLogic;

            _processes = new List<Process>();
            _observers = new List<IObserver<Process>>();

            clientProcess1.Subscribe(this);
        }

        public IDisposable Subscribe(IObserver<Process> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                _processes = (List<Process>)_processLogic.GetAllAsync().Result;

                foreach (var process in _processes)
                {
                    observer.OnNext(process);
                }
            }
            return new DisposableSubscription<Process>(_observers, observer);
        }

        public void Run(Process process)
        {
            process.ProcessState = ProcessState.Queued;

            _processLogic.CreateAsync(process); 
            _processes = (List<Process>)_processLogic.GetAllAsync().Result; 

            if (_observers.Count > 0)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNext(process);
                }
            }
        }
    }
}
