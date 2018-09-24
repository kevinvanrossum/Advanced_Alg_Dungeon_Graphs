using Advanced_Alg_Dungeon_Graphs.Builders;
using Advanced_Alg_Dungeon_Graphs.Controllers;
using Advanced_Alg_Dungeon_Graphs.Factories;
using Advanced_Alg_Dungeon_Graphs.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Advanced_Alg_Dungeon_Graphs_Unit_Tests.Algorithms
{
  public class BreadthFirstSearchTest
  {
    private readonly DungeonController _controller;
    private readonly IDungeon _dungeon;
    public BreadthFirstSearchTest()
    {
      var serviceProvider = new ServiceCollection()
                  .AddSingleton<IMonsterFactory, MonsterFactory>()
                  .AddSingleton<IHallwayFactory, HallwayFactory>()
                  .AddSingleton<IRoomFactory, RoomFactory>()
                  .AddSingleton<IDungeonFactory, DungeonFactory>()
                  .AddSingleton<IDungeonBuilder, DungeonBuilder>()
                  .BuildServiceProvider();

      _controller = new DungeonController(serviceProvider);

      _controller.CreateTestDungeon();
    }

    [Fact]
    public void ItCanSearchBreadthFirst()
    {
    // Info: 5x5 dungeon with start 0,0 and end 4,4 SHOULD result in a value of 8.
    // Info: 10x10 dungeon with start 0,0 and end 4,4 SHOULD STILL result in a value of 8.
    // Info: 10x10 dungeon with start 0,0 and end 9,9 SHOULD result in a value of 18.
      var value = _controller.ActivateTalisman();
      Assert.NotStrictEqual(-1, value);
      Assert.Equal(8, value);
    }

  }
}
