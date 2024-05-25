using Toboggan.Syntax;

namespace Toboggan;

public class Lexer(string text)
{
    private readonly string _text = text ?? string.Empty;
    private int _pos = 0;

    private char Current => Peek(0);
    private char LookAhead => Peek(1);

    private char Peek(int offset = 0) => _pos + offset >= _text.Length ? '\0' : _text[_pos + offset];

    public SyntaxToken Lex()
    {
        if (Current == '\0') return new SyntaxToken(TokenKind.EOF, _pos, "\0");

        var start = _pos;

        if (char.IsWhiteSpace(Current))
        {
            _pos++;
            while (char.IsWhiteSpace(Current)) _pos++;
            var raw = _text[start.._pos];
            return new SyntaxToken(TokenKind.WHITESPACE, start, raw);
        }

        if (char.IsDigit(Current))
        {
            _pos++;
            while (char.IsDigit(Current)) _pos++;
            var raw = _text[start.._pos];
            var value = int.Parse(raw);
            return new SyntaxToken(TokenKind.NUMBER, start, raw, value);
        }

        if (char.IsLetter(Current))
        {
            _pos++;
            while (char.IsLetterOrDigit(Current)) _pos++;
            var raw = _text[start.._pos];

            var tokenKind = GetTokenKind(raw);

            return new SyntaxToken(tokenKind, start, raw, raw);
        }

        if (Current == '+') return new SyntaxToken(TokenKind.PLUS, _pos++, "+");
        else if (Current == '-') return new SyntaxToken(TokenKind.DASH, _pos++, "-");
        else if (Current == '*') return new SyntaxToken(TokenKind.STAR, _pos++, "*");
        else if (Current == '/') return new SyntaxToken(TokenKind.SLASH, _pos++, "/");
        else if (Current == '(') return new SyntaxToken(TokenKind.OPEN_PARANTHESES, _pos++, "(");
        else if (Current == ')') return new SyntaxToken(TokenKind.CLOSE_PARANTHESES, _pos++, ")");
        else if (Current == '!')
        {
            if (LookAhead == '=')
            {
                _pos += 2;
                return new SyntaxToken(TokenKind.NOT_EQUALS, start, "!=");
            }
            return new SyntaxToken(TokenKind.NOT, _pos++, "!");
        }
        else if (Current == '|')
        {
            if (LookAhead == '|')
            {
                _pos += 2;
                return new SyntaxToken(TokenKind.DOUBLE_OR, start, "||");
            }
        }
        else if (Current == '&')
        {
            if (LookAhead == '&')
            {
                _pos += 2;
                return new SyntaxToken(TokenKind.DOUBLE_AND, start, "&&");
            }
        }
        else if (Current == '=')
        {
            if (LookAhead == '=')
            {
                _pos += 2;
                return new SyntaxToken(TokenKind.DOUBLE_EQUALS, start, "==");
            }
            return new SyntaxToken(TokenKind.EQUALS, _pos++, "=");
        }

        return new SyntaxToken(TokenKind.INVALID, start, _text[_pos++..]);
    }

    private TokenKind GetTokenKind(string raw)
    {
        return raw switch
        {
            "true" => TokenKind.TRUE,
            "false" => TokenKind.FALSE,
            _ => TokenKind.IDENTIFIER,
        };
    }
}