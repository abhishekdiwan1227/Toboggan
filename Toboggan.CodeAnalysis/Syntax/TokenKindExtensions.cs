using System.Collections;

namespace Toboggan.Syntax;

public static class TokenKindExtensions
{
    public static int GetUnaryOperatorPrecedence(this TokenKind kind)
    {
        return kind switch
        {
            TokenKind.PLUS or TokenKind.DASH or TokenKind.NOT => 6,
            _ => 0,
        };
    }

    public static int GetBinaryOperatorPrecedence(this TokenKind kind)
    {
        return kind switch
        {
            TokenKind.SLASH or TokenKind.STAR => 5,
            TokenKind.PLUS or TokenKind.DASH => 4,
            TokenKind.DOUBLE_EQUALS or TokenKind.NOT_EQUALS => 3,
            TokenKind.DOUBLE_AND => 2,
            TokenKind.DOUBLE_OR => 1,
            _ => 0,
        };
    }
}