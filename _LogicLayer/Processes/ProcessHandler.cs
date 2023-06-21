using _LogicLayer.Processes.Process1;
using Back._LogicLayer.Logic;
using Common.Models.Processes;

namespace _LogicLayer.Processes
{
    public class ProcessHandler : IObservable<Process>
    {
        private List<IObserver<Process>> _observers;
        private readonly ILogic<Process> _processLogic;

        public ProcessHandler(
            ILogic<Process> processLogic,
            ClientProcess1 clientProcess1)
        {
            _processLogic = processLogic;
            _observers = new List<IObserver<Process>>();

            clientProcess1.Subscribe(this);
        }

        public IDisposable Subscribe(IObserver<Process> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                var processes = GetAllAsync().Result
                    .Where(v => v.ProcessState != ProcessState.Success);

                foreach (var process in processes)
                {
                    observer.OnNext(process);
                }
            }
            return new DisposableSubscription<Process>(_observers, observer);
        }

        public void CreateSpecificProcess(Process process)
        {
            process.ProcessState = ProcessState.Queued;
            _processLogic.CreateAsync(process).ConfigureAwait(false);

            if (_observers.Count <= 0) return;
            foreach (var observer in _observers)
            {
                observer.OnNext(process);
            }
        }

        public void RunSpecificProcess(Process process)
        {
            if (_observers.Count <= 0) return;
            foreach (var observer in _observers)
            {
                observer.OnNext(process);
            }
        }

        public async Task<IEnumerable<Process>> GetAllAsync()
        {
            return await _processLogic.GetAllAsync();
        }

        public async Task  DropCollectionAsync()
        {
            await _processLogic.DropCollectionAsync(); 
        }
    }
}
