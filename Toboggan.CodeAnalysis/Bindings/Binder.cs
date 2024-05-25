using System.ComponentModel;
using Toboggan.Symbols;
using Toboggan.Syntax;

namespace Toboggan.Bindings;

public class Binder(IDictionary<VariableSymbol, object?> variables)
{
    private readonly IDictionary<VariableSymbol, object?> _variables = variables;

    public BoundExpression Bind(Expression expression) => expression.Kind switch
    {
        TokenKind.ASSIGNMENT_EXPRESSION => BindParanthesesExpression((AssignmentExpression)expression),
        TokenKind.PARANTHESES_EXPRESSION => BindParanthesesExpression((ParanthesesExpression)expression),
        TokenKind.LITERAL_EXPRESSION => BindLiteralExpression((LiteralExpression)expression),
        TokenKind.NAME_EXPRESSION => BindNameExpression((NameExpression)expression),
        TokenKind.UNARY_EXPRESSION => BindUnaryExpression((UnaryExpression)expression),
        TokenKind.BINARY_EXPRESSION => BindBinaryExpression((BinaryExpression)expression),
        _ => throw new NotSupportedException(expression.Kind.ToString())
    };

    private BoundBinaryExpression BindBinaryExpression(BinaryExpression expression)
    {
        var boundLeftOperand = Bind(expression.Left);
        var boundRightOperand = Bind(expression.Right);
        var boundBinaryOperator = BoundBinaryOperator.Bind(expression.OperatorToken.Kind, boundLeftOperand.Type, boundRightOperand.Type);

        if (boundBinaryOperator is null) throw new Exception(string.Format("Invalid operation between {0} and {1}", boundLeftOperand.Type, boundRightOperand.Type));

        return new BoundBinaryExpression(boundLeftOperand, boundBinaryOperator, boundRightOperand);
    }

    private BoundUnaryExpression BindUnaryExpression(UnaryExpression expression)
    {
        var boundOperand = Bind(expression.Operand);
        var boundUnaryOperator = BoundUnaryOperator.Bind(expression.OperatorToken.Kind, boundOperand.Type);

        if (boundUnaryOperator is null) throw new Exception(string.Format("Invalid operation {0}", boundOperand.Type));

        return new BoundUnaryExpression(boundOperand, boundUnaryOperator);
    }

    private BoundNameExpression BindNameExpression(NameExpression expression)
    {
        var name = expression.Identifier.Text;
        var variable = _variables.Keys.SingleOrDefault(x => x.Name == name);
        if (variable is null) throw new ArgumentException(name);

        return new BoundNameExpression(variable);
    }

    private BoundLiteralExpression BindLiteralExpression(LiteralExpression expression)
    {
        var value = expression.Val ?? 0;
        return new BoundLiteralExpression(value);
    }

    private BoundExpression BindParanthesesExpression(ParanthesesExpression expression)
    {
        return Bind(expression.Expression);
    }

    private BoundAssignmentExpression BindParanthesesExpression(AssignmentExpression expression)
    {
        var name = expression.Identifier.Text;
        var boundExpression = Bind(expression.Value);

        var existingVar = _variables.Keys.SingleOrDefault(x => x.Name == name);
        if (existingVar != null)
        {
            _variables.Remove(existingVar);
        }
        var newVar = new VariableSymbol(name, boundExpression.Type);
        _variables[newVar] = null;

        return new BoundAssignmentExpression(newVar, boundExpression);
    }
}
