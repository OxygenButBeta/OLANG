using OLANG.Core;

namespace OLANG.Syntax;

public abstract record SyntaxNode;

public abstract record ExpressionNode : SyntaxNode;

public record LiteralExpressionNode(Token LiteralToken, object Value) : ExpressionNode;

public record NameExpressionNode(Token IdentifierToken) : ExpressionNode;

public record AssignmentExpressionNode(Token IdentifierToken, Token EqualsToken, ExpressionNode Expression)
    : ExpressionNode;

public record BinaryExpressionNode(
    ExpressionNode Left,
    Token OperatorToken,
    ExpressionNode Right) : ExpressionNode;


public record CallExpressionNode(
    Token IdentifierToken, 
    Token OpenParenToken, 
    List<ExpressionNode> Arguments, 
    Token CloseParenToken
) : ExpressionNode;