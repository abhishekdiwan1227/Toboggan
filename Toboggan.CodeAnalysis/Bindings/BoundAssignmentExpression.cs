
using Toboggan.Symbols;

namespace Toboggan.Bindings;

public class BoundAssignmentExpression(VariableSymbol symbol, BoundExpression boundExpression) : BoundExpression
{
    public override Type Type => BoundExpression.Type;

    public override BoundNodeKind Kind => BoundNodeKind.ASSIGNMENT_EXPRESSION;

    public VariableSymbol Symbol { get; } = symbol;
    public BoundExpression BoundExpression { get; } = boundExpression;
    public string Name => Symbol.Name;
}