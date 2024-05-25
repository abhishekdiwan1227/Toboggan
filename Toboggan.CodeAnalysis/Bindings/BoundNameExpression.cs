using Toboggan.Symbols;

namespace Toboggan.Bindings;

public class BoundNameExpression(VariableSymbol variable) : BoundExpression
{
    public string Name => Variable.Name;
    public override Type Type => Variable.Type;

    public override BoundNodeKind Kind => BoundNodeKind.NAME_EXPRESSION;

    public VariableSymbol Variable { get; } = variable;
}