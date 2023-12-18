namespace Bidouille.Decorator;

public class DecoratorB : AbstractDecorator
{
    public DecoratorB(AbstractClassToBeDecorated classToBeDecorated) : base(classToBeDecorated)
    {
    }

    public override string Name { get; set; }

    public override string Do()
    {
        return $"azert {base.Do()}";
    }
}