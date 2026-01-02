using System.Drawing;
using OLANG.Runtime;

namespace OLANG.CLI;

internal class Program {
    static void Main(string[] args) {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("OLANG CLI & INTERPRETER");
        Console.WriteLine("========================================");

        OlangRuntime runtime = new();
        Console.WriteLine("Binding Native Functions");
        runtime.NativeFunctions.Bind("min", new Func<double, double, double>(Math.Min));
        runtime.NativeFunctions.Bind("max", new Func<double, double, double>(Math.Max));
        runtime.NativeFunctions.Bind("sqrt", new Func<double, double>(Math.Sqrt));
        runtime.NativeFunctions.Bind("pow", new Func<double, double, double>(Math.Pow));
        runtime.NativeFunctions.Bind("abs", new Func<double, double>(Math.Abs));
        runtime.NativeFunctions.Bind("print",
            new Action<object>(Console.WriteLine));
        Seperator();
        Console.WriteLine("Creating AST");
        runtime.Parse(NestedModelTest.Script);
        Seperator();
        Console.WriteLine("Executing Script");
        Seperator();
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Cyan;
        runtime.Execute();
        Console.ForegroundColor = color;
        Seperator();
        Console.WriteLine("Execution Finished. Dumping Core");
        Seperator();
        runtime.NativeFunctions.Dump();
    }

    static void Seperator() {
        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("___________________________________");
        Console.ForegroundColor = originalColor;
    }
}