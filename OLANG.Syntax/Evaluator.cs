using System.Reflection;
using OLANG.Core;

namespace OLANG.Syntax;

public class Evaluator {
    readonly OlangMemory env;
    readonly List<ExpressionNode> nodes;
    public Evaluator(List<ExpressionNode> nodes, OlangMemory environment) {
        this.nodes = nodes;
        env = environment;
    }
    public object? Evaluate() {
        object? lastValue = null;
        foreach (var node in nodes) {
            lastValue = EvaluateExpression(node);
        }
        return lastValue; 
    }

    object? EvaluateExpression(ExpressionNode node) {
        if (node is LiteralExpressionNode n) return n.Value;
        if (node is NameExpressionNode name) return env.Get(name.IdentifierToken.Text);
    
        if (node is AssignmentExpressionNode assignment) {
            var value = EvaluateExpression(assignment.Expression);
            env.Bind(assignment.IdentifierToken.Text, value);
            return value;
        }
    
        if (node is CallExpressionNode call) {
            var funcName = call.IdentifierToken.Text;
            var func = env.Get(funcName);
            if (func is Delegate nativeMethod) {
                var args = call.Arguments.Select(EvaluateExpression).ToArray();
                return nativeMethod.DynamicInvoke(args);
            }
            throw new Exception($"Function '{funcName}' not found.");
        }

        if (node is BinaryExpressionNode b) {
            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);
            double l = Convert.ToDouble(left);
            double r = Convert.ToDouble(right);

            return b.OperatorToken.Type switch {
                TokenType.Plus => l + r,
                TokenType.Minus => l - r,
                TokenType.Star => l * r,
                TokenType.Slash => l / r,
                TokenType.Greater => l > r,
                TokenType.Less => l < r,
                _ => throw new Exception("Unknown Operator")
            };
        }

        throw new Exception($"Unexpected node type: {node.GetType().Name}");
    }
    static int GetBinaryOperatorPrecedence(TokenType type) {
        return type switch {
            TokenType.Star or TokenType.Slash => 3,
            TokenType.Plus or TokenType.Minus => 2,
            TokenType.EqualsEquals or TokenType.BangEquals or 
                TokenType.Less or TokenType.LessOrEquals or 
                TokenType.Greater or TokenType.GreaterOrEquals => 1,
            _ => 0
        };
    }
}