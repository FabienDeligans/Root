namespace Common.Models.Processes;

public class DisposableSubscription<TProcess> : IDisposable
{
    private readonly List<IObserver<TProcess>> _observers;
    private readonly IObserver<TProcess> _observer;
    public DisposableSubscription(List<IObserver<TProcess>> observers, IObserver<TProcess> observer)
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