
namespace Toboggan.Syntax;

public class NameExpression(SyntaxToken identifier, string? name) : Expression
{
    public override TokenKind Kind => TokenKind.NAME_EXPRESSION;

    public override IEnumerable<SyntaxNode> Children => [Identifier];

    public SyntaxToken Identifier { get; } = identifier;
    public string? Name { get; } = name;
}
