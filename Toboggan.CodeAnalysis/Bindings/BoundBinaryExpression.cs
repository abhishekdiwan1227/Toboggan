
using Toboggan.Syntax;

namespace Toboggan.Bindings;

public class BoundBinaryExpression(BoundExpression left, BoundBinaryOperator boundOperator, BoundExpression right) : BoundExpression
{
    public override Type Type => BinaryOperator.OutType;

    public override BoundNodeKind Kind => BoundNodeKind.BINARY_EXPRESSION;

    public BoundExpression Left { get; } = left;
    public BoundBinaryOperator BinaryOperator { get; } = boundOperator;
    public BoundExpression Right { get; } = right;
}

public class BoundBinaryOperator(TokenKind operatorKind, BoundBinaryOperatorKind boundOperatorKind, Type leftType, Type rightType, Type outType)
{
    public BoundBinaryOperator(TokenKind operatorKind, BoundBinaryOperatorKind boundOperatorKind, Type type) : this(operatorKind, boundOperatorKind, type, type, type) { }
    public BoundBinaryOperator(TokenKind operatorKind, BoundBinaryOperatorKind boundOperatorKind, Type inType, Type outType) : this(operatorKind, boundOperatorKind, inType, inType, outType) { }

    public TokenKind OperatorKind { get; } = operatorKind;
    public BoundBinaryOperatorKind BoundOperatorKind { get; } = boundOperatorKind;
    public Type LeftType { get; } = leftType;
    public Type RightType { get; } = rightType;
    public Type OutType { get; } = outType;

    private static BoundBinaryOperator[] _operators =
    [
        new BoundBinaryOperator(TokenKind.PLUS, BoundBinaryOperatorKind.ADD, typeof(int)),
        new BoundBinaryOperator(TokenKind.DASH, BoundBinaryOperatorKind.SUB, typeof(int)),
        new BoundBinaryOperator(TokenKind.SLASH, BoundBinaryOperatorKind.DIV, typeof(int)),
        new BoundBinaryOperator(TokenKind.STAR, BoundBinaryOperatorKind.MUL, typeof(int)),
        new BoundBinaryOperator(TokenKind.DOUBLE_EQUALS, BoundBinaryOperatorKind.EQUALITY, typeof(int), typeof(bool)),
        new BoundBinaryOperator(TokenKind.NOT_EQUALS, BoundBinaryOperatorKind.INEQUALITY, typeof(int), typeof(bool)),
        new BoundBinaryOperator(TokenKind.DOUBLE_EQUALS, BoundBinaryOperatorKind.EQUALITY, typeof(bool), typeof(bool)),
        new BoundBinaryOperator(TokenKind.NOT_EQUALS, BoundBinaryOperatorKind.INEQUALITY, typeof(bool), typeof(bool)),
        new BoundBinaryOperator(TokenKind.DOUBLE_AND, BoundBinaryOperatorKind.LOGICAL_ADD, typeof(bool)),
        new BoundBinaryOperator(TokenKind.DOUBLE_OR, BoundBinaryOperatorKind.LOGICAL_OR, typeof(bool)),
    ];

    public static BoundBinaryOperator? Bind(TokenKind operatorKind, Type leftType, Type rightType) => _operators.SingleOrDefault(x => x.OperatorKind == operatorKind && x.LeftType == leftType && x.RightType == rightType);
}
