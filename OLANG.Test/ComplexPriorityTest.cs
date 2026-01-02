/*using OLANG.Core;
using OLANG.Syntax;

public static class ComplexPriorityTest
{
    public static void Begin()
    {
        Console.WriteLine("--- [Test] Complex Priority & Parentheses ---");
        var input = "(10 + 2) * 3 - 10 / 2"; 
        
        List<Token> tokens = Scan(input);
        Parser parser = new(tokens.ToArray());
        ExpressionNode tree = parser.Parse();
        
        OlangMemory env = new();
        var result = new Evaluator(tree, env).Evaluate();

        Console.WriteLine($"Input: {input}");
        Console.WriteLine($"Result: {result} | Expected: 31");
    }

    static List<Token> Scan(string input) {
        Lexer lexer = new(input);
        var tokens = new List<Token>();
        while (true) {
            Token t = lexer.NextToken();
            if (t.Type == TokenType.EOF) break;
            tokens.Add(t);
        }
        return tokens;
    }
}*/