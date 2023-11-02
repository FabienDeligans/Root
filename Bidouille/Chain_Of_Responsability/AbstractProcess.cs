namespace Bidouille.Chain_Of_Responsability;

public abstract class AbstractProcess : IProcess
{
    public abstract IProcess NextProcess { get; set; }

    public IProcess SetNext(IProcess nextProcess)
    {
        NextProcess = nextProcess;
        return nextProcess;
    }

    public abstract IProcess Handle(object? obj);
}