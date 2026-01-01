namespace OLANG.Core;

public record Diagnostic(string Message, int Position);

public class DiagnosticBag : IEnumerable<Diagnostic>
{
    private readonly List<Diagnostic> _diagnostics = new();

    public void Report(string message, int position) => _diagnostics.Add(new Diagnostic(message, position));
    
    public void ReportInvalidCharacter(int position, char character) 
        => Report($"Bad character input: '{character}'", position);

    public void ReportUnexpectedToken(int position, TokenType actual, TokenType expected)
        => Report($"Unexpected token <{actual}>, expected <{expected}>", position);

    public IEnumerator<Diagnostic> GetEnumerator() => _diagnostics.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}