
namespace Toboggan.Syntax;

public class LiteralExpression(SyntaxToken numberToken, object? val) : Expression
{
    public override TokenKind Kind => TokenKind.LITERAL_EXPRESSION;

    public override IEnumerable<SyntaxNode> Children => [NumberToken];

    public SyntaxToken NumberToken { get; } = numberToken;
    public object? Val { get; } = val;
}
