public class OlangMemory {
    readonly Func<object, bool> FilterFunc;
    readonly string name;

    public OlangMemory(string Name = "Unnamed Memory", Func<object, bool>? filterFunc = null) {
        name = Name;
        FilterFunc = filterFunc ?? ((_) => true);
    }

    readonly Dictionary<string, object> variables = new();

    public void Bind(string name, object value) {
        if (!FilterFunc(value))
            throw new InvalidOperationException(
                $"Value of type '{value.GetType().Name}' is not allowed in OlangMemory.");

        variables[name] = value;
    }

    public object? Get(string name) {
        variables.TryGetValue(name, out var value);
        return value;
    }

    public void Dump() {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n====[{name} DUMP]====");

        if (variables.Count == 0)
            Console.WriteLine("[Empty MEMORY]");
        else
            foreach (KeyValuePair<string, object> kvp in variables) {
                var typeName = kvp.Value?.GetType().Name ?? "null";
                Console.WriteLine($"  {kvp.Key,-12} : {kvp.Value,-8} [{typeName}]");
            }

        Console.WriteLine("=====================\n");
        Console.ResetColor();
    }
}