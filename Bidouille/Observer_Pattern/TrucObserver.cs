namespace Bidouille.Observer_Pattern
{
    public class TrucObserver<T> : IObserver<T>
    {
        private IDisposable _disposableSubscription;
        private List<T> _dataList;

        public TrucObserver()
        {
            _dataList = new List<T>();
        }

        public void Subscribe(TrucHandler<T> subscription)
        {
            _disposableSubscription = subscription.Subscribe(this);
        }

        public void UnSubscribe()
        {
            _disposableSubscription.Dispose();
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
                // Do something
                Console.WriteLine(data);
            }
            _dataList.Remove(value);
        }
    }
}
