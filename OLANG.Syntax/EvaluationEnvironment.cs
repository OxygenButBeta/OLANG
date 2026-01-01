public class EvaluationEnvironment 
{
    readonly Dictionary<string, object> variables = new();

    public void Assign(string name, object value) => variables[name] = value;

    public object? Get(string name) 
    {
        variables.TryGetValue(name, out var value);
        return value;
    }
}