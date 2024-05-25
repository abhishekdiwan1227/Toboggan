
namespace Toboggan.Syntax;

public class UnaryExpression(SyntaxToken operatorToken, Expression operand) : Expression
{
    public SyntaxToken OperatorToken { get; } = operatorToken;
    public Expression Operand { get; } = operand;

    public override TokenKind Kind => TokenKind.UNARY_EXPRESSION;

    public override IEnumerable<SyntaxNode> Children => [OperatorToken, Operand];

}