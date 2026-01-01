namespace OLANG.Core;

public record Token(
    TokenType Type, 
    string Text, 
    object? Value, 
    int Position)
{
    public override string ToString() => $"{Type}: '{Text}'";
}