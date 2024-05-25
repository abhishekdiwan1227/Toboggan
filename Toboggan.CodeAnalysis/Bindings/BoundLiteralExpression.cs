namespace Toboggan.Bindings;

public class BoundLiteralExpression(object value) : BoundExpression
{
    public override Type Type => Value.GetType();

    public override BoundNodeKind Kind => BoundNodeKind.LITERAL_EXPRESSION;

    public object Value { get; } = value;
}