/*using OLANG.Core;
using OLANG.Syntax;

public static class MegaIntegrationTest {
    public static void Begin() {
        Console.WriteLine("=== [OLANG MEGA INTEGRATION TEST] ===");
        OlangMemory env = new();

        env.Assign("clamp", new Func<double, double, double, double>((v, min, max) => Math.Clamp(v, min, max)));
        env.Assign("lerp", new Func<double, double, double, double>((a, b, t) => a + (b - a) * Math.Clamp(t, 0, 1)));
        env.Assign("dist2d", new Func<double, double, double, double, double>((x1, y1, x2, y2) => 
            Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2))));
        env.Assign("print", new Action<object>(val => Console.WriteLine($"> [LOG]: {val}")));

        string[] script = {
            "player_x = 100",
            "player_y = 150",
            "enemy_x = 105",
            "enemy_y = 155",
            
            "max_distance = 10",
            "actual_dist = dist2d(player_x, player_y, enemy_x, enemy_y)",
            
            "is_in_range = actual_dist <= max_distance",
            "print(is_in_range)", "base_hp = 100",
            "raw_damage = 45",
            "defense = 12", "damage_multiplier = 1", "final_hp = base_hp - (clamp(raw_damage - defense, 0, 100) * damage_multiplier)",
            
            "health_perc = final_hp / base_hp",
            "print(health_perc)", "ui_red_channel = lerp(255, 0, health_perc)",
            "print(ui_red_channel)"
        };

        foreach (var line in script) {
            List<Token> tokens = Scan(line);
            Parser parser = new(tokens.ToArray());
            ExpressionNode tree = parser.Parse();
            new Evaluator(tree, env).Evaluate();
        }
        
        env.Dump();
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
}*/