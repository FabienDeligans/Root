using System.Text.Json;

namespace Bidouille.Observer_Pattern
{
    public abstract class Observer<T> : IObserver<T>
    {
        private IDisposable _subscription;
        private List<T> _dataList;

        public Observer()
        {
            _dataList = new List<T>();
        }

        public void Subscribe(Handler<T> subscription)
        {
            _subscription = subscription.Subscribe(this);
        }

        public void UnSubscribe()
        {
            _subscription.Dispose();
            _dataList.Clear();
        }

        public void OnCompleted()
        {
            _dataList.Clear();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(T value)
        {
            if (_dataList.Contains(value)) return;

            _dataList.Add(value);
            foreach (var data in _dataList)
            {
               DoInObserver(data); 
            }
            _dataList.Remove(value);
        }

        public abstract void DoInObserver(T value);
    }

    public class ConcreteObserver : Observer<object>
    {
        public override void DoInObserver(object value)
        {
            Console.WriteLine($"Do in observer : {JsonSerializer.Serialize(value)}");
        }
    }
}
