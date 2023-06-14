namespace Back._LogicLayer.Events
{
    public class MyCustomEvent : EventArgs, ICustomEvent
    {
        public string ValueEvent { get; set; }
    }
}
