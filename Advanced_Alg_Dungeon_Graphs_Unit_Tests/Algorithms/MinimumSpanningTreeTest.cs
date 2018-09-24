using Advanced_Alg_Dungeon_Graphs.Builders;
using Advanced_Alg_Dungeon_Graphs.Controllers;
using Advanced_Alg_Dungeon_Graphs.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Algorithms
{
  public class MinimumSpanningTreeTest
  {
    private readonly DungeonController _controller;
    public MinimumSpanningTreeTest()
    {
      var serviceProvider = new ServiceCollection()
                  .AddSingleton<IMonsterFactory, MonsterFactory>()
                  .AddSingleton<IHallwayFactory, HallwayFactory>()
                  .AddSingleton<IRoomFactory, RoomFactory>()
                  .AddSingleton<IDungeonFactory, DungeonFactory>()
                  .AddSingleton<IDungeonBuilder, DungeonBuilder>()
                  .BuildServiceProvider();

      _controller = new DungeonController(serviceProvider);
    }

    [Fact]
      public void ItShouldNotFail()
      {
          _controller.CreateTestDungeon(5, 5, true);
          var result = _controller.ActivateGrenade();
          Assert.NotStrictEqual(-1, result);
          
      }
  }
}
