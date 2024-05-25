namespace Toboggan.Syntax;

public class SyntaxToken(TokenKind kind, int position, string text, object? val = null) : SyntaxNode
{
    public override TokenKind Kind => kind;
    public int Position{ get; set; } = position;
    public string Text{ get; set; } = text;
    public object? Value { get; set; } = val;

    public override IEnumerable<SyntaxNode> Children => Enumerable.Empty<SyntaxNode>();
}