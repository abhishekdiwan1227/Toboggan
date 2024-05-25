namespace Toboggan.Syntax;

public abstract class SyntaxNode
{
    public abstract TokenKind Kind {get; }
    public abstract IEnumerable<SyntaxNode> Children { get; }
}
