namespace Library.Events
{
    public class MyCustomEvent : EventArgs, ICustomEvent
    {
        public string ValueEvent { get; set; }
    }
}
