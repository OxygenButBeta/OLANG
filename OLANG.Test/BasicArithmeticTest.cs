/*using OLANG.Core;
using OLANG.Syntax;

namespace OLANG.CLI.Tests;

public static class BasicArithmeticTest
{
    public static void Begin()
    {
        Console.WriteLine("=== OLANG EXECUTION TRACE ===");

        var input = "a = 10 + 5 * 2"; 
        Console.WriteLine($"1. Input: {input}");

        Lexer lexer = new(input);
        var tokens = new List<Token>();
        while (true) 
        {
            Token token = lexer.NextToken();
            if (token.Type == TokenType.EOF) break;
            tokens.Add(token);
        }
        Console.WriteLine($"2. Tokens Created: {tokens.Count} tokens found.");

        Parser parser = new(tokens.ToArray());
        ExpressionNode syntaxTree = parser.Parse();
        Console.WriteLine($"3. Syntax Tree: Root node is {syntaxTree.GetType().Name}");

        OlangMemory env = new();

        Evaluator evaluator = new(syntaxTree, env);
        var result = evaluator.Evaluate();

        Console.WriteLine($"4. Final Result: {result}");
        Console.WriteLine("=============================");
    }
}*/