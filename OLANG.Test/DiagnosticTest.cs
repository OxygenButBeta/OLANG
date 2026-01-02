using OLANG.Core;
using OLANG.Syntax;

public static class DiagnosticTest
{
    public static void Begin()
    {
        Console.WriteLine("--- [Test] Diagnostic & Error Handling ---");
        var inputs = new[] { "a = 10 ? 5", "10 + * 5" };

        foreach (var input in inputs)
        {
            Console.WriteLine($"\nTesting Input: {input}");
            Lexer lexer = new(input);
            var tokens = new List<Token>();
            while (true) {
                Token t = lexer.NextToken();
                if (t.Type == TokenType.EOF) break;
                tokens.Add(t);
            }

            Parser parser = new(tokens.ToArray());
            parser.Parse();

            List<Diagnostic> diagnostics = lexer.Diagnostics.Concat(parser.Diagnostics).ToList();
            if (diagnostics.Any())
            {
                foreach (Diagnostic d in diagnostics)
                    Console.WriteLine($"[Detected Error]: {d.Message} at position {d.Position}");
            }
        }
    }
}