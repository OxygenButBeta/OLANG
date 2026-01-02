using OLANG.Runtime;

public static class NestedModelTest {
    public const string Script = """

                                                 player_x = 10
                                                 player_y = 20
                                                 enemy_x = 13
                                                 enemy_y = 24
                                                 
                                                 dist = sqrt(pow(enemy_x - player_x, 2) + pow(enemy_y - player_y, 2))
                                                 base_damage = 100 / dist
                                                 armor = 15
                                                 final_damage = max(0, base_damage - armor)
                                                 
                                                 print(dist)
                                                 print(base_damage)
                                                 print(final_damage)
                                             
                                 """;
}