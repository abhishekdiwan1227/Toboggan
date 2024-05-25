
namespace Toboggan.Syntax;

public class AssignmentExpression(SyntaxToken identifier, Expression value) : Expression
{
    public override TokenKind Kind => TokenKind.ASSIGNMENT_EXPRESSION;

    public override IEnumerable<SyntaxNode> Children => [Identifier, Value];

    public SyntaxToken Identifier { get; } = identifier;
    public Expression Value { get; } = value;
}
