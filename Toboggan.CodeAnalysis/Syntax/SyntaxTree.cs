namespace Toboggan.Syntax;

public class SyntaxTree(Expression expression, SyntaxToken eofToken)
{
    public Expression Expression { get; set; } = expression;
    public SyntaxToken Eof{ get; set; } = eofToken;
}