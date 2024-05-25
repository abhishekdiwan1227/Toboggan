
namespace Toboggan.Bindings;

public class BoundUnaryExpression(BoundExpression expression, BoundUnaryOperator unaryOperator) : BoundExpression
{
    public BoundExpression Expression { get; } = expression;
    public BoundUnaryOperator UnaryOperator { get; } = unaryOperator;

    public override Type Type => UnaryOperator.OutType;

    public override BoundNodeKind Kind => BoundNodeKind.UNARY_EXPRESSION;
}