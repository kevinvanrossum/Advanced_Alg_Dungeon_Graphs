using System;
using Advanced_Alg_Dungeon_Graphs.Factories;
using Advanced_Alg_Dungeon_Graphs.Models;
using Advanced_Alg_Dungeon_Graphs.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced_Alg_Dungeon_Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IMonsterFactory, MonsterFactory>()
                .AddSingleton<IHallwayFactory, HallwayFactory>()
                .AddSingleton<IRoomFactory, RoomFactory>()
                .AddSingleton<IDungeonFactory, DungeonFactory>()
                .BuildServiceProvider();

            var dungeonController = new DungeonController(serviceProvider);
            dungeonController.Explore();
        }
    }
}