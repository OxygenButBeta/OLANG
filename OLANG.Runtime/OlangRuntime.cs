using OLANG.Core;
using OLANG.Syntax;

namespace OLANG.Runtime;

public class OlangRuntime {
    public List<OLangAssembly> LoadedLibraries { get; } = [];
    public OlangMemory NativeFunctions { get; } = new(nameof(NativeFunctions));
    List<ExpressionNode> statements;
    List<Token> tokens;

    public void Execute() {
        Evaluator evaluator = new(statements, NativeFunctions);
        evaluator.Evaluate();
    }

    public bool TryCompile( string sourceCode) {
        tokens = Scan(sourceCode);
        Parser parser = new(tokens.ToArray());
        statements = parser.ParseCompilationUnit();
        return true;
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

public class OLangAssembly {
    public string GetVersion() => "1.0.0";
    public List<OLangTypeDefinition> Types { get; } = [];

    public OLangAssembly() {
    }
}

public class OLangTypeDefinition {
    public string TypeName { get; set; } = "";
    public List<OLangFuncDefinition> Methods { get; } = [];
    public List<OLangFieldDefinition> Fields { get; } = [];
}

public record OLangParameterDefinition {
    public string Name { get; set; } = "";
    public string Type { get; set; } = "object";
}

public record OLangFuncDefinition {
    public string ReturnType { get; set; } = "void";
    public string MethodName { get; set; } = "";
    public List<OLangParameterDefinition> Parameters { get; } = [];
}

public record OLangFieldDefinition {
    public string FieldName { get; set; } = "";
    public string FieldType { get; set; } = "object";
    public bool IsStatic { get; set; } = false;
}