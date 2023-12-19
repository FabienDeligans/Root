using System.Text.Json;

namespace Bidouille.Observer_Pattern
{
    public abstract class Handler<T> : IObservable<T>
    {
        private List<T> _listDeTruc;
        private List<IObserver<T>> _observers;

        public Handler()
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
            return new Subscription<T>(_observers, observer);
        }

        public virtual void Do(T data)
        {
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

    public class ConcreteHandler : Handler<object>
    {
        public override void Do(object data)
        {
            Console.WriteLine($"Do in handler : {JsonSerializer.Serialize(data)}");
            base.Do(data);
        }
    }
}
