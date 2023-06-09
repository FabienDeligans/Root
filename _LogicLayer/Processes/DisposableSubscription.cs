namespace _LogicLayer.Processes;

public class DisposableSubscription<Process> : IDisposable
{
    private readonly List<IObserver<Process>> _observers;
    private readonly IObserver<Process> _observer;
    public DisposableSubscription(List<IObserver<Process>> observers, IObserver<Process> observer)
    {
        _observers = observers;
        _observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
        {
            _observers.Remove(_observer);
        }
    }
}