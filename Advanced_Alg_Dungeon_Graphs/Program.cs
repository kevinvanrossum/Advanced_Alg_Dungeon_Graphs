using Advanced_Alg_Dungeon_Graphs.Builders;
using Advanced_Alg_Dungeon_Graphs.Controllers;
using Advanced_Alg_Dungeon_Graphs.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced_Alg_Dungeon_Graphs
{
    internal static class Program
    {
        private static void Main()
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IMonsterFactory, MonsterFactory>()
                .AddSingleton<IHallwayFactory, HallwayFactory>()
                .AddSingleton<IRoomFactory, RoomFactory>()
                .AddSingleton<IDungeonFactory, DungeonFactory>()
                .AddSingleton<IDungeonBuilder, DungeonBuilder>()
                .BuildServiceProvider();

            var dungeonController = new DungeonController(serviceProvider);
            
            // Boot our dungeon!
            dungeonController.Explore();
        }
    }
}
