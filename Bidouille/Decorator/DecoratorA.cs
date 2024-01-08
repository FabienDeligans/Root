namespace Bidouille.Decorator;

public class DecoratorA : AbstractDecorator
{
    public DecoratorA(AbstractClassToBeDecorated classToBeDecorated) : base(classToBeDecorated)
    {
    }

    public override string Name { get; set; }

    public override string Do()
    {
        return $"Decorator A | {base.Do()}";
    }
}