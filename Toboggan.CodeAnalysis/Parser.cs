using Toboggan.Syntax;

namespace Toboggan;

public class Parser
{
    private int _pos = 0;
    private SyntaxToken[] _tokens;

    public Parser(string text)
    {
        var lexer = new Lexer(text);

        var tokens = new List<SyntaxToken>();

        SyntaxToken? token = null;
        do
        {
            token = lexer.Lex();

            if (token.Kind != TokenKind.WHITESPACE && token.Kind != TokenKind.INVALID)
                tokens.Add(token);
        } while (token.Kind != TokenKind.EOF);

        _tokens = tokens.ToArray();
    }

    private SyntaxToken Peek(int offset) => _pos + offset >= _tokens.Length ? _tokens[^1] : _tokens[_pos + offset];

    private SyntaxToken Current => Peek(0);

    private SyntaxToken LookAhead => Peek(1);

    private SyntaxToken Match(TokenKind kind)
    {
        if (kind == Current.Kind) return Next();
        else throw new Exception(string.Format("Expected token {0}", kind));
    }

    private SyntaxToken Next()
    {
        var token = Current;
        _pos++;
        return token;
    }


    private Expression ParseExpression(int parentPrecedence = 0)
    {

        if (Current.Kind == TokenKind.IDENTIFIER && LookAhead.Kind == TokenKind.EQUALS)
        {
            var identifier = Next();
            _ = Match(TokenKind.EQUALS);
            var value = ParseExpression();

            return new AssignmentExpression(identifier, value);
        }

        Expression left;

        var precedence = Current.Kind.GetUnaryOperatorPrecedence();
        if (precedence != 0 && precedence >= parentPrecedence)
        {
            var operatorToken = Next();
            var operand = ParseExpression(parentPrecedence);
            left = new UnaryExpression(operatorToken, operand);
        }
        else
        {
            if (Current.Kind == TokenKind.OPEN_PARANTHESES)
            {
                var innerLeft = Next();
                var expression = ParseExpression();
                var innerRight = Match(TokenKind.CLOSE_PARANTHESES);

                left = new ParanthesesExpression(innerLeft, expression, innerRight);
            }
            else if (Current.Kind == TokenKind.TRUE || Current.Kind == TokenKind.FALSE)
            {
                var val = Current.Kind == TokenKind.TRUE;
                var token = Next();

                left = new LiteralExpression(token, val);
            }
            else if (Current.Kind == TokenKind.IDENTIFIER)
            {
                var identifier = Next();

                left = new NameExpression(identifier, identifier.Text);
            }
            else
            {
                var number = Match(TokenKind.NUMBER);
                left = new LiteralExpression(number, number.Value);
            }
        }

        while (true)
        {
            precedence = Current.Kind.GetBinaryOperatorPrecedence();
            if (precedence == 0 || precedence <= parentPrecedence) break;

            var operatorToken = Next();
            var right = ParseExpression(precedence);

            left = new BinaryExpression(left, operatorToken, right);
        }

        return left;
    }

    public SyntaxTree Parse()
    {
        var expression = ParseExpression();
        var eofToken = Match(TokenKind.EOF);

        return new SyntaxTree(expression, eofToken);
    }
}