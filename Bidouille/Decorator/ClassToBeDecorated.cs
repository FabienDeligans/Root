namespace Bidouille.Decorator;

public class ClassToBeDecorated : AbstractClassToBeDecorated
{
    public override string Name { get; set; }
    public override string Do()
    {
        return "azert"; 
    }
}