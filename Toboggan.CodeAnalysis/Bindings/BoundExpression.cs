namespace Toboggan.Bindings;

public abstract class BoundExpression: BoundNode
{
    public abstract Type Type { get; }
}