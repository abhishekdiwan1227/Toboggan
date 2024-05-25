
namespace Toboggan.Syntax;

public class ParanthesesExpression(SyntaxToken open, Expression expression, SyntaxToken close) : Expression
{
    public override TokenKind Kind => TokenKind.PARANTHESES_EXPRESSION;

    public override IEnumerable<SyntaxNode> Children => [Open, Expression, Close];

    public SyntaxToken Open { get; } = open;
    public Expression Expression { get; } = expression;
    public SyntaxToken Close { get; } = close;
}