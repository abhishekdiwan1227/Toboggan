using Toboggan.Syntax;

namespace Toboggan.Bindings;

public class BoundUnaryOperator(TokenKind operatorKind, BoundUnaryOperatorKind boundOperatorKind, Type inType, Type outType)
{
    public BoundUnaryOperator(TokenKind operatorKind, BoundUnaryOperatorKind boundOperatorKind, Type type) : this(operatorKind, boundOperatorKind, type, type) { }

    public TokenKind OperatorKind { get; } = operatorKind;
    public BoundUnaryOperatorKind BoundOperatorKind { get; } = boundOperatorKind;
    public Type InType { get; } = inType;
    public Type OutType { get; } = outType;

    private static BoundUnaryOperator[] _operators =
    [
        new BoundUnaryOperator(TokenKind.NOT, BoundUnaryOperatorKind.LOGICAL_NEG, typeof(bool)),
        new BoundUnaryOperator(TokenKind.DASH, BoundUnaryOperatorKind.NEG, typeof(int)),
        new BoundUnaryOperator(TokenKind.PLUS, BoundUnaryOperatorKind.ID, typeof(int))
    ];

    public static BoundUnaryOperator? Bind(TokenKind operatorKind, Type inType) => _operators.SingleOrDefault(x => x.OperatorKind == operatorKind && x.InType == inType);
}
