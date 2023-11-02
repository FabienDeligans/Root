namespace Bidouille.Observer_Pattern
{
    public class TrucHandler<T> : IObservable<T>
    {
        private List<T> _listDeTruc;
        private List<IObserver<T>> _observers;

        public TrucHandler()
        {
            _listDeTruc = new List<T>();
            _observers = new List<IObserver<T>>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                foreach (var truc in _listDeTruc)
                {
                    observer.OnNext(truc);
                }
            }
            return new DisposableSubscription<T>(_observers, observer);
        }

        public void Do(T data)
        {
            // Do something

            _listDeTruc.Add(data);
            if (_observers.Count == 0) return;

            foreach (var observer in _observers)
            {
                observer.OnNext(data);
            }
        }

        public void End()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
            _observers.Clear();
        }
    }
}
