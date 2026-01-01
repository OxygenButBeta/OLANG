using OLANG.Core;
using OLANG.Syntax;

public static class SequentialMemoryTest
{
    public static void Begin()
    {
        Console.WriteLine("--- [Test] Sequential Memory Trace ---");

        string[] script = {
            "radius = 10",
            "pi = 3",
            "area = pi * radius * radius"
        };
        EvaluationEnvironment env = new();

        foreach (var line in script)
        {
            
            List<Token> tokens = Scan(line);
            ExpressionNode tree = new Parser(tokens.ToArray()).Parse();
            var result = new Evaluator(tree, env).Evaluate();
            Console.WriteLine($"Step: {line} => Current Value: {result}");
        }

        Console.WriteLine($"Final 'area' in Memory: {env.Get("area")}");
    }

    static List<Token> Scan(string input) {
        Lexer lexer = new(input);
        var tokens = new List<Token>();
        while (true) {
            Token token = lexer.NextToken();
            tokens.Add(token);
            if (token.Type == TokenType.EOF) break;
        }
        return tokens;
    }
}