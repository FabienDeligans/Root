namespace Bidouille.Event_Pattern;

public class MyCustomEvent : EventArgs, ICustomEvent
{
    public string ValueEvent { get; set; }
}