using Toboggan.Bindings;
using Toboggan.Symbols;

namespace Toboggan;

public class Evaluator(BoundExpression root, IDictionary<VariableSymbol, object?> variables)
{
    private readonly IDictionary<VariableSymbol, object?> _variables = variables;

    public object Evaluate()
    {
        return EvaluateExpression(root);
    }

    private object EvaluateExpression(BoundExpression node)
    {
        if (node is BoundLiteralExpression l) return l.Value;

        if (node is BoundNameExpression n)
        {
            var value = _variables[n.Variable];
            if(value is null) throw new Exception(string.Format("Invalid variable usage: {0}"));
            return value;
        }

        if (node is BoundAssignmentExpression a)
        {
            var value = EvaluateExpression(a.BoundExpression);
            _variables[a.Symbol] = value;
            return value;
        }

        if (node is BoundUnaryExpression u)
        {
            var operand = EvaluateExpression(u.Expression);

            return u.UnaryOperator.BoundOperatorKind switch
            {
                BoundUnaryOperatorKind.ID => (int)operand,
                BoundUnaryOperatorKind.NEG => -(int)operand,
                BoundUnaryOperatorKind.LOGICAL_NEG => !(bool)operand,
                _ => throw new Exception(string.Format("Unexpected unary operator enncoutered: {0}", u.UnaryOperator.BoundOperatorKind)),
            };
        }

        if (node is BoundBinaryExpression b)
        {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            return b.BinaryOperator.BoundOperatorKind switch
            {
                BoundBinaryOperatorKind.ADD => (int)left + (int)right,
                BoundBinaryOperatorKind.SUB => (int)left - (int)right,
                BoundBinaryOperatorKind.MUL => (int)left * (int)right,
                BoundBinaryOperatorKind.DIV => (int)left / (int)right,
                BoundBinaryOperatorKind.LOGICAL_ADD => (bool)left && (bool)right,
                BoundBinaryOperatorKind.LOGICAL_OR => (bool)left || (bool)right,
                BoundBinaryOperatorKind.EQUALITY => Equals(left, right),
                BoundBinaryOperatorKind.INEQUALITY => !Equals(left, right),
                _ => throw new Exception(string.Format("Unexpected binary operator encountered: {0}", b.BinaryOperator.BoundOperatorKind)),
            };
        }

        throw new Exception(string.Format("Unexpected node", node.Kind));
    }
}
