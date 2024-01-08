namespace Bidouille.Decorator;

public abstract class AbstractDecorator : AbstractClassToBeDecorated
{
    protected AbstractClassToBeDecorated _classToBeDecorated;

    public AbstractDecorator(AbstractClassToBeDecorated classToBeDecorated)
    {
        _classToBeDecorated = classToBeDecorated;
    }

    public override string Do()
    {
        return _classToBeDecorated.Do();
    }
}