using OLANG.CLI.Tests;

namespace OLANG.CLI;

internal class Program {
    static void Main(string[] args) {
        while (true) {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("   OLANG CLI & INTERPRETER");
            Console.WriteLine("========================================");


            EngineLogicTest.Begin();


            Console.WriteLine("\n-------------------------");
            Console.ReadKey();
        }
    }
}