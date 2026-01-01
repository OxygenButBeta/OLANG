using OLANG.Core;
using OLANG.Syntax;

namespace OLANG.CLI.Tests;

public static class MethodTest {
    public static void Begin() {
        Console.WriteLine("--- [Test] Method Call & Native Functions ---");
        EvaluationEnvironment env = new();

        env.Assign("print", new Action<object>((val) => Console.WriteLine($"> OUT (Extern Call): {val}")));
        
        env.Assign("max", new Func<int, int, int>(Math.Max));

        string[] script = {
            "a = 15",
            "b = 25",
            "print(a + b)", "m = max(a, b * 2)", "print(m)"
        };

        foreach (var line in script) {
            List<Token> tokens = Scan(line);
            ExpressionNode tree = new Parser(tokens.ToArray()).Parse();
            var result = new Evaluator(tree, env).Evaluate();
            Console.WriteLine($"Executed: {line} | Result: {result}");
        }
    }

    static List<Token> Scan(string input) {
        Lexer lexer = new(input);
        var tokens = new List<Token>();
        while (true) {
            Token t = lexer.NextToken();
            tokens.Add(t);
            if (t.Type == TokenType.EOF) break;
        }

        return tokens;
    }
}