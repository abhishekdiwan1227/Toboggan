
namespace Toboggan.Syntax;

public class BinaryExpression(Expression left, SyntaxToken operatorToken, Expression right) : Expression
{
    public override TokenKind Kind => TokenKind.BINARY_EXPRESSION;

    public override IEnumerable<SyntaxNode> Children => [Left, OperatorToken, Right];

    public Expression Left { get; } = left;
    public SyntaxToken OperatorToken { get; } = operatorToken;
    public Expression Right { get; } = right;
}