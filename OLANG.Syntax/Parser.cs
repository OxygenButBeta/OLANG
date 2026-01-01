using System.Reflection;
using OLANG.Core;

namespace OLANG.Syntax;

public class Parser {
    public DiagnosticBag Diagnostics { get; } = new();
    readonly Token[] tokens;
    int position;

    public Parser(Token[] tokens) {
        this.tokens = tokens;
    }

    Token Current => Peek(0);

    Token Peek(int offset) {
        var index = position + offset;

        if (tokens.Length == 0)
            return new Token(TokenType.EOF, "\0", null, 0);

        if (index >= tokens.Length)
            return tokens[^1];

        return tokens[index];
    }

    Token Match(TokenType type) {
        if (Current.Type == type) return tokens[position++];

        Diagnostics.ReportUnexpectedToken(Current.Position, Current.Type, type);
        return new Token(type, "", null, Current.Position);
    }

    Token NextToken() {
        Token current = Current;
        position++;
        return current;
    }

    public ExpressionNode Parse() {
        return ParseExpression(0);
    }

    static int GetBinaryOperatorPrecedence(TokenType type) {
        return type switch {
            TokenType.Star or TokenType.Slash => 2,
            TokenType.Plus or TokenType.Minus => 1,
            _ => 0
        };
    }
    public ExpressionNode ParseExpression(int parentPrecedence = 0) {
        if (Peek(0).Type == TokenType.Identifier && Peek(1).Type == TokenType.Equals) {
            Token identifierToken = NextToken();
            Token operatorToken = NextToken();
            ExpressionNode right = ParseExpression();
            return new AssignmentExpressionNode(identifierToken, operatorToken, right);
        }

        ExpressionNode left = ParsePrimary();

        while (true) {
            var precedence = GetBinaryOperatorPrecedence(Current.Type);

            if (precedence == 0 || precedence <= parentPrecedence)
                break;

            Token operatorToken = NextToken();

            ExpressionNode right = ParseExpression(precedence);

            left = new BinaryExpressionNode(left, operatorToken, right);
        }

        return left;
    }

    ExpressionNode ParsePrimary() {
        if (Current.Type == TokenType.OpenParen) {
            position++;
            ExpressionNode expression = ParseExpression();
            Match(TokenType.CloseParen);
            return expression;
        }

        if (Current.Type == TokenType.Identifier) {
            Token identifier = NextToken();

            if (Current.Type == TokenType.OpenParen) {
                Token openParen = NextToken();
                var arguments = new List<ExpressionNode>();

                if (Current.Type != TokenType.CloseParen) {
                    while (true) {
                        arguments.Add(ParseExpression());
                        if (Current.Type != TokenType.Comma) break;
                        NextToken();
                    }
                }

                Token closeParen = Match(TokenType.CloseParen);
                return new CallExpressionNode(identifier, openParen, arguments, closeParen);
            }

            return new NameExpressionNode(identifier);
        }


        Token numberToken = Match(TokenType.Number);
        return new LiteralExpressionNode(numberToken, numberToken.Value!);
    }
}

public class Evaluator {
    readonly ExpressionNode root;
    readonly EvaluationEnvironment env = new();

    public Evaluator(ExpressionNode root, EvaluationEnvironment environment) {
        env = environment;
        this.root = root;
    }

    public object Evaluate() {
        return EvaluateExpression(root);
    }

    object? EvaluateExpression(ExpressionNode node) {
        if (node is LiteralExpressionNode n)
            return n.Value;

        if (node is NameExpressionNode name)
            return env.Get(name.IdentifierToken.Text);

        if (node is AssignmentExpressionNode assignment) {
            var value = EvaluateExpression(assignment.Expression);
            env.Assign(assignment.IdentifierToken.Text, value);
            return value;
        }
        
        if (node is CallExpressionNode call) {
            var funcName = call.IdentifierToken.Text;
            var func = env.Get(funcName);

            if (func is Delegate nativeMethod) {
                var args = call.Arguments.Select(EvaluateExpression).ToArray();

                ParameterInfo[] methodParams = nativeMethod.Method.GetParameters();
                if (methodParams.Length != args.Length) {
                    throw new Exception($"Function '{funcName}' expects {methodParams.Length} arguments, but got {args.Length}.");
                }

                return nativeMethod.DynamicInvoke(args);
            }
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
                TokenType.Slash => r == 0 ? throw new Exception("Division by zero") : l / r,
                _ => throw new Exception($"Unexpected operator {b.OperatorToken.Type}")
            };
        }

        return null;
    }
}